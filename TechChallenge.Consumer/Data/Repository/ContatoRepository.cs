using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fiap.Clientes.API.Models;
using Fiap.Core.Data;

namespace Fiap.Clientes.API.Data.Repository
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly ContatoContext _context;

        public ContatoRepository(ContatoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Contato>> ObterTodos()
        {
            return await _context.Clientes.AsNoTracking().ToListAsync();
        }

        public void Adicionar(Contato cliente)
        {
            _context.Clientes.Add(cliente);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}