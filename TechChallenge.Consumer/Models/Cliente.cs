using Fiap.Core.DomainObjects;

namespace Fiap.Clientes.API.Models
{
    public class Contato : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }

        // EF Relation
        protected Contato() { }

        public Contato(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}