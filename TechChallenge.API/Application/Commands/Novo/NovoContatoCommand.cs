using Fiap.Core.DomainObjects;
using Fiap.Core.Messages;
using FluentValidation;

namespace Fiap.Clientes.API.Application.Commands
{
    public class NovoContatoCommand : Command
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }

        public NovoContatoCommand(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public override bool EhValido()
        {
            ValidationResult = new RegistrarClienteValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegistrarClienteValidation : AbstractValidator<NovoContatoCommand>
        {
            public RegistrarClienteValidation()
            {
                RuleFor(c => c.Nome)
                    .NotEmpty()
                    .WithMessage("O nome do cliente não foi informado");
            }

            protected static bool TerEmailValido(string email)
            {
                return Core.DomainObjects.Email.Validar(email);
            }
        }
    }
}