using MediatR;

namespace Fiap.Clientes.API.Application.Events
{
    public class ContatoEventHandler : INotificationHandler<ContatoRegistradoEvent>
    {
        public Task Handle(ContatoRegistradoEvent notification, CancellationToken cancellationToken)
        {
            // Enviar evento de confirmação
            return Task.CompletedTask;
        }
    }
}