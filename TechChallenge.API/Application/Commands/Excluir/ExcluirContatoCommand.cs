using Fiap.Core.Messages;
using FluentValidation;

namespace Fiap.Clientes.API.Application.Commands
{
    public class ExcluirContatoCommand : Command
    {
        public Guid Id { get; private set; }

        public ExcluirContatoCommand(Guid id)
        {
            Id = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new ExcluirContatoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class ExcluirContatoValidation : AbstractValidator<ExcluirContatoCommand>
        {
            public ExcluirContatoValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do cliente inválido");
            }
        }
    }
}