using Fiap.Api.Alunos.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.Alunos.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<AlertaModel> Alertas { get; set; }

        protected void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<AlertaModel>(entity =>
            {
                entity.ToTable("Alertas");
                entity.HasKey(e => e.Id);

                // Configura o Id como coluna de identidade
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Nome)
                      .IsRequired();

                entity.Property(e => e.Localizacao);
                entity.Property(e => e.Descricao);

                entity.Property(e => e.Data)
                      .HasColumnType("DATE");
            });

        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        protected DatabaseContext()
        {
        }
    }

}

