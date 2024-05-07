using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;
using TechChallengeFIAP.Infrastracture.Data;

namespace TechChallengeFIAP.Infrastructure.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly FiapDbContext _dbContext;

        public ContatoRepository(FiapDbContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Contato> AddAsync(Contato contato)
        {
            _dbContext.Add(contato);
            await _dbContext.SaveChangesAsync();
            return contato;
        }

        public async Task DeleteAsync(Contato contato)
        {
            var contatoDelete = await FindAsync(contato.Id);

            if (contatoDelete is not null)
                _dbContext.Contatos.Remove(contatoDelete);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Contato> FindAsync(int ID)
        {
            var contato = await _dbContext.Contatos.FindAsync(ID);

            if (contato == null)
                throw new InvalidOperationException($"Contato com o Id: {ID} não encontrado.");

            return contato;
        }

        public async Task<IEnumerable<Contato>> GetAllAsync(string? DDD)
        {
            var contatos = await _dbContext.Contatos.Where(x => DDD == x.Telefone.DDD || DDD == null).ToListAsync();

            if (contatos == null || contatos.Count == 0)
                throw new InvalidOperationException($"Contatos com o DDD: {(DDD is null ? "nulo" : DDD)} não encontrado.");

            return contatos;

        }

        public async Task UpdateAsync(Contato contato)
        {
            _dbContext.Update(contato);
            await _dbContext.SaveChangesAsync();
        }
    }
}
