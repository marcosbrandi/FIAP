using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Core.Entities;

namespace TechChallengeFIAP.Infrastracture.Data
{
    public class FiapDbContext : DbContext
    {
        public DbSet<Contato> Contatos { get; set; }

        public FiapDbContext(DbContextOptions<FiapDbContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=fiap.db;");
        //}

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        */
    }
}
