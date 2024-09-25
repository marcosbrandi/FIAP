using Fiap.Core.Data;
using Microsoft.EntityFrameworkCore;
using TechChallenge.Core.Entities;
using TechChallenge.Core.Interfaces;
using TechChallenge.Infrastracture.Data;

namespace TechChallenge.Infrastructure.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly FiapDbContext _context;

        public ContatoRepository(FiapDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        /// <summary>
        /// Busca e retorna um contato pelo seu registro no banco, recebendo um id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Contato> FindAsync(Guid id)
        {
            return await _context.Contatos.FindAsync(id);
        }

        public async Task<Contato?> GetByIdWithPhone(Guid id)
        {
            return await _context.Contatos.Include(x => x.Telefone).FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Faz uma busca de todos os contatos de acordo com o DDD inserido
        /// </summary>
        /// <param name="DDD"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<IEnumerable<Contato>> GetAllAsync(string? ddd)
        {
            return await _context.Contatos.Include(x => x.Telefone).Where(x => ddd == x.Telefone.Ddd || ddd == null).ToListAsync(); 
        }

        /// <summary>
        /// Retorna a quantidade de registros inseridos na base
        /// </summary>
        /// <returns></returns>
        public async Task<int> CountAsync()
        {
            return await _context.Contatos.CountAsync();
        }

        /// <summary>
        /// Registra um contato na base recebendo
        /// TODO: Verificar como criar método de consulta de Contato por e-mail
        /// </summary>
        /// <param name="contato"></param>
        /// <returns></returns>
        public async Task<Contato> AddAsync(Contato contato)
        {
            await _context.AddAsync(contato);
            return contato;
        }

        /// <summary>
        /// Atualiza o contato
        /// </summary>
        /// <param name="contato"></param>
        /// <returns></returns>
        public void Update(Contato contato)
        {
            _context.Contatos.Update(contato);
        }

        /// <summary>
        /// Deleta um contato recebendo o id como parâmetro para encontrar o contato
        /// </summary>
        /// <param name="contato"></param>
        /// <returns></returns>
        public void Delete(Contato contato)
        {
            _context.Contatos.Remove(contato);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
