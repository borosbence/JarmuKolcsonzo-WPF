using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace JarmuKolcsonzo.Models
{
    public partial class JKContext : DbContext
    {
        public JKContext()
        {
        }

        public JKContext(DbContextOptions<JKContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Jarmu> Jarmuvek { get; set; }
        public virtual DbSet<JarmuTipus> Jarmu_tipusok { get; set; }
        public virtual DbSet<Rendeles> Rendelesek { get; set; }
        public virtual DbSet<Ugyfel> Ugyfelek { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = ConfigurationManager.ConnectionStrings["JKDB"].ConnectionString;
                optionsBuilder.UseMySql(connectionString, ServerVersion.Parse("10.4.21-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            modelBuilder.Entity<Jarmu>(entity =>
            {
                entity.Property(e => e.rendszam)
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.HasOne(d => d.tipus)
                    .WithMany(p => p.jarmu)
                    .HasForeignKey(d => d.tipus_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("jarmu_ibfk_1");
            });

            modelBuilder.Entity<Rendeles>(entity =>
            {
                entity.Property(e => e.ar).HasPrecision(7);

                entity.HasOne(d => d.jarmu)
                    .WithMany(p => p.rendeles)
                    .HasForeignKey(d => d.jarmu_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rendeles_ibfk_2");

                entity.HasOne(d => d.ugyfel)
                    .WithMany(p => p.rendeles)
                    .HasForeignKey(d => d.ugyfel_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rendeles_ibfk_1");
            });

            modelBuilder.Entity<Ugyfel>(entity =>
            {
                entity.Property(e => e.pont).HasPrecision(4, 2);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
