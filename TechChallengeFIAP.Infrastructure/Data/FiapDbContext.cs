using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Core.Entities;

namespace TechChallengeFIAP.Infrastracture.Data
{
    public class FiapDbContext : DbContext
    {
        public FiapDbContext(DbContextOptions<FiapDbContext> options) : base(options) { }

        public DbSet<Contato> Contatos { get; set; }
    }
}
