using Fiap.Clientes.API.Application.Events;
using Fiap.Clientes.API.Models;
using Fiap.Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace Fiap.Clientes.API.Application.Commands
{
    public class ContatoCommandHandler : CommandHandler, IRequestHandler<RegistrarContatoCommand, ValidationResult>
    {
        private readonly IContatoRepository _clienteRepository;

        public ContatoCommandHandler(IContatoRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ValidationResult> Handle(RegistrarContatoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var cliente = new Contato(message.Id, message.Nome);

            //var clienteExistente = await _clienteRepository.ObterPorCpf(cliente.Cpf.Numero);

            //if (clienteExistente != null)
            //{
            //    AdicionarErro("Este CPF já está em uso.");
            //    return ValidationResult;
            //}

            _clienteRepository.Adicionar(cliente);

            cliente.AdicionarEvento(new ContatoRegistradoEvent(message.Id, message.Nome));

            return await PersistirDados(_clienteRepository.UnitOfWork);
        }
    }
}