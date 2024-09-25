using Fiap.Core.Data;
using TechChallenge.Core.Entities;

namespace TechChallenge.Core.Interfaces
{
    public interface IContatoRepository : IRepository<Contato>
    {
        public Task<IEnumerable<Contato>> GetAllAsync(string? ddd);
        public Task<Contato?> GetByIdWithPhone(Guid id);
        public Task<Contato> FindAsync(Guid id);
        public Task<int> CountAsync();
        public Task<Contato> AddAsync(Contato contato);
        public void Update(Contato contato);
        public void Delete(Contato contato);
    }
}