using GreenDriveApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenDriveApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bateria> Baterias { get; set; }
        public DbSet<EstacaoCarga> EstacoesCarga { get; set; }
        public DbSet<OrdemReciclagem> OrdensReciclagem { get; set; }
        public DbSet<RegistroTelemetria> RegistrosTelemetria { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bateria>()
                .Property(b => b.NumeroSerie)
                .IsRequired();

            modelBuilder.Entity<EstacaoCarga>()
                .Property(e => e.Cidade)
                .IsRequired();

            modelBuilder.Entity<EstacaoCarga>()
                .Property(e => e.TipoCarga)
                .IsRequired();

            modelBuilder.Entity<OrdemReciclagem>()
                .Property(o => o.CustoProcessamento)
                .HasColumnType("decimal(10,2)");
        }
    }
}