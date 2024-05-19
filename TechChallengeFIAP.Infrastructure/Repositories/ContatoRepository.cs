using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;
using TechChallengeFIAP.Infrastracture.Data;

namespace TechChallengeFIAP.Infrastructure.Repositories
{
    public class ContatoRepository(FiapDbContext fiapContext) : IContatoRepository
    {
        private readonly DbSet<Contato> db = fiapContext.Set<Contato>();

        
        /// <summary>
        /// Registra um contato na base recebendo
        /// TODO: Verificar como criar método de consulta de Contato por e-mail
        /// </summary>
        /// <param name="contato"></param>
        /// <returns></returns>
        public async Task<Contato> AddAsync(Contato contato)
        {
            bool emailregistrado = await CheckRegisteredEmail(contato);
            if (emailregistrado)
            fiapContext.Add(contato);
            await fiapContext.SaveChangesAsync();
            return contato;
            
        }
        
        /// <summary>
        /// Deleta um contato recebendo o id como parâmetro para encontrar o contato
        /// </summary>
        /// <param name="contato"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Contato contato)
        {
            var contatoDelete = await FindAsync(contato.Id);

            if (contatoDelete is null)
                throw new WarningException("Contato não foi encontrado");

                fiapContext.Contatos.Remove(contatoDelete);

            await fiapContext.SaveChangesAsync();
        }


        /// <summary>
        /// Busca e retorna um contato pelo seu registro no banco, recebendo um id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Contato> FindAsync(int ID)
        {
            var contato = await fiapContext.Contatos.Include(x => x.Telefone).FirstOrDefaultAsync(x=> x.Id==ID); //  FindAsync(ID);
            //var contato = await fiapContext.Telefones.Where(x => DDD == x.DDD || DDD == null).Select(a => a.Contato).ToListAsync();

            if (contato == null)
                throw new WarningException($"Contato com o Id: {ID} não encontrado.");

            return contato;
        }

        /// <summary>
        /// Checa se o e-mail inserido já foi cadastrado no sistema recebendo string email como parâmetro
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="WarningException"></exception>
        public async Task<bool> CheckRegisteredEmail(Contato contato)
        {
            int contatos = CountAsync().Result;
            if (contatos > 0)
            {
                var emailChecker = fiapContext.Contatos.Where(c => c.Email == contato.Email);


                if(emailChecker.Any())
                throw new WarningException($"O email inserido já está cadastrado");
            }
            return true;
        }

        /// <summary>
        /// Faz uma busca de todos os contatos de acordo com o DDD inserido
        /// </summary>
        /// <param name="DDD"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<IEnumerable<Contato>> GetAllAsync(string? DDD)
        {
            var contatos = await fiapContext.Contatos.Include(x=> x.Telefone).Where(x => DDD == x.Telefone.DDD || DDD == null).ToListAsync();
            //var contatos = await fiapContext.Telefones.Where(x => DDD == x.DDD || DDD == null).Select(a=>a.Contato).ToListAsync();

            if (contatos == null || (contatos.Count == 0 && DDD is not null))
                throw new InvalidOperationException($"Contatos com o DDD: {(DDD is null ? "nulo" : DDD)} não encontrado.");

            return contatos;

        }

        /// <summary>
        /// Atualiza o contato
        /// </summary>
        /// <param name="contato"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Contato contato)
        {
            fiapContext.Update(contato);
            await fiapContext.SaveChangesAsync();
        }

        /// <summary>
        /// Retorna a quantidade de registros inseridos na base
        /// </summary>
        /// <returns></returns>
        public async Task<int> CountAsync()
        {
            return await db.CountAsync();
        }
    }
}
