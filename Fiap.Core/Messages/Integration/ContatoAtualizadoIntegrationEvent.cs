namespace Fiap.Core.Messages.Integration
{
    public class ContatoAtualizadoIntegrationEvent : IntegrationEvent
    {
        public ContatoAtualizadoIntegrationEvent(Guid id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
    }
}