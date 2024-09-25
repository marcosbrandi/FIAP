using Fiap.Core.Data;

namespace Fiap.Clientes.API.Models
{
    public interface IContatoRepository : IRepository<Contato>
    {
        void Adicionar(Contato cliente);
        Task<IEnumerable<Contato>> ObterTodos();
    }
}