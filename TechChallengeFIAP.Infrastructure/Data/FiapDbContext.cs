using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Core.Entities;

namespace TechChallengeFIAP.Infrastracture.Data
{
    public class FiapDbContext : DbContext
    {
        //public FiapDbContext(DbContextOptions<FiapDbContext> options) : base(options) { }
        public FiapDbContext(DbContextOptions<FiapDbContext> options) { } 
        public DbSet<Contato> Contatos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=fiap.db;");
        }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        */
    }
}
