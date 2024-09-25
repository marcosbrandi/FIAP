using Fiap.Core.Messages.Integration;
using Fiap.Identidade.API.Models;
using Fiap.MessageBus;
using Fiap.WebAPI.Core.Controllers;
using Fiap.WebAPI.Core.Identidade;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace TechChallenge.API.Controllers
{
    [Route("api/Contato")]
    public class ContatoController : MainController
    {
        private readonly AppSettings _appSettings;

        private readonly IMessageBus _bus;

        public ContatoController(IOptions<AppSettings> appSettings, IMessageBus bus)
        {
            _appSettings = appSettings.Value;
            _bus = bus;
        }

        [HttpPost("")]
        public async Task<ActionResult> Contato(ContatoRegistroRequest contatoRegistroRequest)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var clienteResult = await RegistrarCliente(contatoRegistroRequest);

            //if (!clienteResult.ValidationResult.IsValid)
            //{
            //    await _userManager.DeleteAsync(user);
            //    return CustomResponse(clienteResult.ValidationResult);
            //}

            return CustomResponse(clienteResult);
        }

        private async Task<ResponseMessage> RegistrarCliente(ContatoRegistroRequest contatoRegistroRequest)
        {
            var contatoRegistrado = new ContatoRegistradoIntegrationEvent(Guid.NewGuid(), contatoRegistroRequest.Nome);

            try
            {
                return await _bus.RequestAsync<ContatoRegistradoIntegrationEvent, ResponseMessage>(contatoRegistrado);
            }
            catch
            {
                //await _userManager.DeleteAsync(usuario);
                throw;
            }
        }
    }
}
