namespace Fiap.Core.Messages.Integration
{
    public class ContatoExcluidoIntegrationEvent : IntegrationEvent
    {
        public ContatoExcluidoIntegrationEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}