using FluentValidation.Results;
using MediatR;

namespace Fiap.Core.Messages
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public DateTime TimeStamp { get; private set; }
        public ValidationResult ValidationResult { get; protected set; }

        protected Command() => TimeStamp = DateTime.Now;

        public virtual bool EhValido() => ValidationResult.IsValid;
    }
}
