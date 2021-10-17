using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AudioTel.Models
{
    public partial class DbContextAudioTel : DbContext
    {
        public DbContextAudioTel()
        {
        }

        public DbContextAudioTel(DbContextOptions<DbContextAudioTel> options)
            : base(options)
        {
        }

        public virtual DbSet<Bancocliente> Bancoclientes { get; set; }
        public virtual DbSet<Gravacoes> Gravacoes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=audiotel;uid=root;pwd=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.26-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Bancocliente>(entity =>
            {
                entity.ToTable("bancocliente");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.DataStatus)
                    .HasColumnType("date")
                    .HasColumnName("dataStatus");

                entity.Property(e => e.Ddd1)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("ddd1");

                entity.Property(e => e.Ddd2)
                    .HasMaxLength(5)
                    .HasColumnName("ddd2");

                entity.Property(e => e.Ddd3)
                    .HasMaxLength(5)
                    .HasColumnName("ddd3");

                entity.Property(e => e.Ddd4)
                    .HasMaxLength(5)
                    .HasColumnName("ddd4");

                entity.Property(e => e.Entrevistador)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("entrevistador");

                entity.Property(e => e.Fone1)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("fone1");

                entity.Property(e => e.Fone2)
                    .HasMaxLength(20)
                    .HasColumnName("fone2");

                entity.Property(e => e.Fone3)
                    .HasMaxLength(20)
                    .HasColumnName("fone3");

                entity.Property(e => e.Fone4)
                    .HasMaxLength(20)
                    .HasColumnName("fone4");

                entity.Property(e => e.Gravacao)
                    .HasMaxLength(100)
                    .HasColumnName("gravacao");

                entity.Property(e => e.HoraStatus)
                    .HasColumnType("time")
                    .HasColumnName("horaStatus");

                entity.Property(e => e.NumTratado).HasColumnName("numTratado");

                entity.Property(e => e.TelFeito)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("telFeito");
            });

            modelBuilder.Entity<Gravacoes>(entity =>
            {
                entity.ToTable("gravacoes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NomeDoArquivo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nomeDoArquivo");

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("numero");

                entity.Property(e => e.Ramal)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("ramal");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
