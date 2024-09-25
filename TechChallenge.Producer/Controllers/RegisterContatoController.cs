using Fiap.Core.Messages.Integration;
using Fiap.MessageBus;
using Fiap.WebAPI.Core.Controllers;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TechChallenge.Producer.Controllers
{
    [Route("api/RegisterContato")]
    public class RegisterContatoController : MainController
    {
        private readonly IMessageBus _bus;

        public RegisterContatoController(IMessageBus bus)
        {
            _bus = bus;
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] NovoContato request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var clienteResult = await RegistrarContato(request);

            return CustomResponse(clienteResult);
        }

        private async Task<ResponseMessage> RegistrarContato(NovoContato request)
        {
            try
            {
                return await _bus.RequestAsync<ContatoRegistradoIntegrationEvent, ResponseMessage>(
                    new ContatoRegistradoIntegrationEvent(request.Nome, request.Email));
            }
            catch (AggregateException ae)
            {
                var validationResult = new ValidationResult();

                validationResult.Errors.Add(new ValidationFailure("AtualizaContato", ae.Message));

                return new ResponseMessage(validationResult);
            }
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AtualizaContato request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (id != request.Id)
            {
                return BadRequest("Erro ao salvar o contato!");
            }

            return CustomResponse(await AtualizaContato(request));
        }

        private async Task<ResponseMessage> AtualizaContato(AtualizaContato request)
        {
            try
            {
                return await _bus.RequestAsync<ContatoAtualizadoIntegrationEvent, ResponseMessage>(
                    new ContatoAtualizadoIntegrationEvent(request.Id, request.Nome, request.Email));
            }
            catch (AggregateException ae)
            {
                var validationResult = new ValidationResult();

                validationResult.Errors.Add(new ValidationFailure("AtualizaContato", ae.Message));

                return new ResponseMessage(validationResult);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var clienteResult = await ExcluirContato(id);

            //if (!clienteResult.ValidationResult.IsValid)
            //{
            //    await _userManager.DeleteAsync(user);
            //    return CustomResponse(clienteResult.ValidationResult);
            //}

            return CustomResponse(clienteResult);
        }

        private async Task<ResponseMessage> ExcluirContato(Guid id)
        {
            try
            {
                return await _bus.RequestAsync<ContatoExcluidoIntegrationEvent, ResponseMessage>(
                    new ContatoExcluidoIntegrationEvent(id));
            }
            catch (AggregateException ae)
            {
                var validationResult = new ValidationResult();

                validationResult.Errors.Add(new ValidationFailure("AtualizaContato", ae.Message));

                return new ResponseMessage(validationResult);
            }
        }

    }
}
