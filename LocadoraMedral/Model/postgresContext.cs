using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LocadoraMedral.Model
{
    public partial class postgresContext : DbContext
    {
        public virtual DbSet<TbCliente> TbCliente { get; set; }
        public virtual DbSet<TbFilme> TbFilme { get; set; }
        public virtual DbSet<TbLocacao> TbLocacao { get; set; }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(@"Host=localhost;Database=postgres;Username=postgres;Password=admin");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("adminpack");

            modelBuilder.Entity<TbCliente>(entity =>
            {
                entity.ToTable("tbCliente");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasColumnName("CPF");

                entity.Property(e => e.Endereco).IsRequired();

                entity.Property(e => e.Nome).IsRequired();
            });

            modelBuilder.Entity<TbFilme>(entity =>
            {
                entity.ToTable("tbFilme");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Filme).IsRequired();

                entity.Property(e => e.Genero).IsRequired();
            });

            modelBuilder.Entity<TbLocacao>(entity =>
            {
                entity.ToTable("tbLocacao");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DataLocacao).HasColumnName("dataLocacao");

                entity.Property(e => e.TbClienteId).HasColumnName("tbClienteID");

                entity.Property(e => e.TbFilmeId).HasColumnName("tbFilmeID");

                entity.HasOne(d => d.TbCliente)
                    .WithMany(p => p.TbLocacao)
                    .HasForeignKey(d => d.TbClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("locacao_tbClienteID_fkey");

                entity.HasOne(d => d.TbFilme)
                    .WithMany(p => p.TbLocacao)
                    .HasForeignKey(d => d.TbFilmeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("locacao_tbFilmeID_fkey");
            });
        }
    }
}