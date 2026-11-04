using Microsoft.EntityFrameworkCore;

namespace Sube;

public class SubeDbContext : DbContext
{
    public SubeDbContext() { }

    public SubeDbContext(DbContextOptions<SubeDbContext> opciones) : base(opciones) { }

    public DbSet<TarjetaTipo> TarjetaTipos => Set<TarjetaTipo>();
    public DbSet<Tarjeta> Tarjetas => Set<Tarjeta>();
    public DbSet<Colectivo> Colectivos => Set<Colectivo>();
    public DbSet<Boleto> Boletos => Set<Boleto>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=sube.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tarjeta>()
            .HasOne(t => t.Tipo)
            .WithMany()
            .HasForeignKey(t => t.Id_Tipo)
            .IsRequired(false);

        modelBuilder.Entity<Boleto>()
            .HasOne(b => b.Tarjeta)
            .WithMany()
            .HasForeignKey(b => b.TarjetaId);

        modelBuilder.Entity<Boleto>()
            .HasOne(b => b.Colectivo)
            .WithMany()
            .HasForeignKey(b => b.ColectivoId);
    }
}
