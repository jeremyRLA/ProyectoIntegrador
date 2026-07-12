using Microsoft.EntityFrameworkCore;
using UTNGolCoinApi.Models;

namespace UTNGolCoinApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Billetera> Billeteras { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }
        public DbSet<Prediccion> Predicciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Billetera>()
                .Property(b => b.Saldo)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Transaccion>()
                .Property(t => t.Monto)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Prediccion>()
                .Property(p => p.MontoApostado)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Prediccion>()
                .Property(p => p.Cuota)
                .HasPrecision(18, 2);
        }
    }
}