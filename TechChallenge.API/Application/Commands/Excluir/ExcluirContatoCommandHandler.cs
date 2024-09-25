using Fiap.Core.Messages;
using FluentValidation.Results;
using MediatR;
using TechChallenge.Core.Interfaces;

namespace Fiap.Clientes.API.Application.Commands
{
    public class ExcluirContatoCommandHandler : CommandHandler, IRequestHandler<ExcluirContatoCommand, ValidationResult>
    {
        private readonly IContatoRepository _clienteRepository;

        public ExcluirContatoCommandHandler(IContatoRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ValidationResult> Handle(ExcluirContatoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var actual = await _clienteRepository.FindAsync(message.Id);

            if (actual == null)
            {
                AdicionarErro("Objeto não localizado!");
                return ValidationResult;
            }

            _clienteRepository.Delete(actual);

            return await PersistirDados(_clienteRepository.UnitOfWork);
        }
    }
}