namespace Fiap.Core.Messages.Integration
{
    public class ContatoRegistradoIntegrationEvent : IntegrationEvent
    {
        public ContatoRegistradoIntegrationEvent(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
    }
}