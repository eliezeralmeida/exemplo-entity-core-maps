using Microsoft.EntityFrameworkCore;

namespace ExemplosEntity.OneToMany
{
    public class OneToManyContext : DbContext
    {
        public OneToManyContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configura Entidade Desenlvovedor
            modelBuilder.Entity<Desenvolvedor>()
                .ToTable("Desenvolvedores")
                .HasKey(d => d.Id);

            modelBuilder.Entity<Desenvolvedor>()
                .Property(d => d.Nome);

            modelBuilder.Entity<Desenvolvedor>()
                .HasOne(d => d.Projeto)
                .WithMany()
                .HasForeignKey(d => d.ProjetoId);

            // configura Entidade Projeto
            modelBuilder.Entity<Projeto>()
                .ToTable("Projetos")
                .HasKey(p => p.Id);

            modelBuilder.Entity<Projeto>()
                .Property(p => p.Nome);
        }

        public DbSet<Desenvolvedor> Desenvolvedores { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
    }
}