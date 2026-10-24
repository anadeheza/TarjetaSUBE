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
}
