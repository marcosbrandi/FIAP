using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Core.Entities;

namespace TechChallengeFIAP.Infrastracture.Data
{
    public class FiapDbContext : DbContext
    {
        public FiapDbContext(DbContextOptions<FiapDbContext> options) : base(options) {

        }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Telefone> Telefones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Telefone>()
                .HasOne(e => e.Contato)
                .WithMany(e => e.Telefones)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            modelBuilder.Entity<Contato>()
                .Navigation(e => e.Telefones)
                .AutoInclude();
            */
            /*
            modelBuilder.Entity<Contato>()
                .HasOne(a => a.Telefone)
                .WithOne(b => b.Contato)
                .HasForeignKey<Telefone>(b => b.TelefoneCompleto);
            */
            //base.OnModelCreating(modelBuilder);
        }

    }
}
