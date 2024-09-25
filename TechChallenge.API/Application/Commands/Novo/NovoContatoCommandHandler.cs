using Fiap.Clientes.API.Application.Events;
using Fiap.Core.Messages;
using FluentValidation.Results;
using MediatR;
using TechChallenge.Core.Entities;
using TechChallenge.Core.Interfaces;

namespace Fiap.Clientes.API.Application.Commands
{
    public class NovoContatoCommandHandler : CommandHandler, IRequestHandler<NovoContatoCommand, ValidationResult>
    {
        private readonly IContatoRepository _clienteRepository;

        public NovoContatoCommandHandler(IContatoRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ValidationResult> Handle(NovoContatoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var cliente = new Contato(message.Nome, message.Email);

            await _clienteRepository.AddAsync(cliente);

            // Quando precisar adicionar algum evento
            cliente.AdicionarEvento(new ContatoRegistradoEvent(Guid.NewGuid(), message.Nome));

            return await PersistirDados(_clienteRepository.UnitOfWork);
        }
    }
}