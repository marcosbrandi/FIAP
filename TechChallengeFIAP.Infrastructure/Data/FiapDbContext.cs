using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Core.Entities;

namespace TechChallengeFIAP.Infrastracture.Data
{
    public class FiapDbContext : DbContext
    {
        public FiapDbContext(DbContextOptions<FiapDbContext> options) : base(options) { }

        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Contato>().HasKey(x => x.Id);
            //modelBuilder.Entity<Contato>().ComplexProperty(c => c.Telefone);
        }

    }
}
