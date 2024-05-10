using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;
using TechChallengeFIAP.Infrastracture.Data;

namespace TechChallengeFIAP.Infrastructure.Repositories
{
    public class ContatoRepository(FiapDbContext fiapContext) : IContatoRepository
    {
        private readonly DbSet<Contato> db = fiapContext.Set<Contato>();

        public async Task<Contato> AddAsync(Contato contato)
        {
            fiapContext.Add(contato);
            await fiapContext.SaveChangesAsync();
            return contato;
        }
        public async Task DeleteAsync(Contato contato)
        {
            var contatoDelete = await FindAsync(contato.Id);

            if (contatoDelete is not null)
                fiapContext.Contatos.Remove(contatoDelete);

            await fiapContext.SaveChangesAsync();
        }

        public async Task<Contato> FindAsync(int ID)
        {
            var contato = await fiapContext.Contatos.Include(x => x.Telefone).FirstOrDefaultAsync(x=> x.Id==ID); //  FindAsync(ID);

            if (contato == null)
                throw new InvalidOperationException($"Contato com o Id: {ID} não encontrado.");

            return contato;
        }

        public async Task<IEnumerable<Contato>> GetAllAsync(string? DDD)
        {
            var contatos = await fiapContext.Contatos.Include(x=> x.Telefone).Where(x => DDD == x.Telefone.DDD || DDD == null).ToListAsync();

            if (contatos == null || (contatos.Count == 0 && DDD is not null))
                throw new InvalidOperationException($"Contatos com o DDD: {(DDD is null ? "nulo" : DDD)} não encontrado.");

            return contatos;

        }
        public async Task UpdateAsync(Contato contato)
        {
            fiapContext.Update(contato);
            await fiapContext.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await db.CountAsync();
        }

    }
}
