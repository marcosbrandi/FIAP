using Fiap.Core.Messages;
using FluentValidation.Results;
using MediatR;
using TechChallenge.Core.Interfaces;

namespace Fiap.Clientes.API.Application.Commands
{
    public class AtualizarContatoCommandHandler : CommandHandler, IRequestHandler<AtualizarContatoCommand, ValidationResult>
    {
        private readonly IContatoRepository _clienteRepository;

        public AtualizarContatoCommandHandler(IContatoRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ValidationResult> Handle(AtualizarContatoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var actual = await _clienteRepository.FindAsync(message.Id);

            if (actual == null)
            {
                AdicionarErro("Objeto não localizado!");
                return ValidationResult;
            }

            actual.Update(message.Nome, message.Email);
            _clienteRepository.Update(actual);

            return await PersistirDados(_clienteRepository.UnitOfWork);
        }
    }
}