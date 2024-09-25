using Microsoft.EntityFrameworkCore;
using TechChallenge.API.Models;

namespace Fiap.Identidade.API.Data
{
    public class FiapContext : DbContext
    {
        public FiapContext(DbContextOptions<FiapContext> options) : base(options)
        {

        }

        public DbSet<Contato> Contatos { get; set; }
    }
}