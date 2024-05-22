using TechChallengeFIAP.Core.Entities;

namespace TechChallengeFIAP.Core.Interfaces
{
    public interface IContatoRepository
    {
        public Task<Contato> AddAsync(Contato contato);
        public Task UpdateAsync(Contato currentContato,Contato updatedContato);
        public Task DeleteAsync(Contato contato);
        public Task<IEnumerable<Contato>> GetAllAsync(string? DDD);
        public Task<Contato> FindAsync(int ID);
        public Task<int> CountAsync();
        public Task<Contato> GetByNameAsync(string nome);

    }
}