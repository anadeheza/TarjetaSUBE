using Microsoft.EntityFrameworkCore;
using Sube;

namespace TarjetaTest;

public class SubeDbContextTests
{
    [Test]
    public void GuardaLasEntidadesEnUnaBaseEnMemoria()
    {
        var opciones = new DbContextOptionsBuilder<SubeDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var db = new SubeDbContext(opciones);
        db.Tarjetas.Add(new Tarjeta());
        db.Colectivos.Add(new Colectivo("102"));
        db.SaveChanges();

        Assert.That(db.Tarjetas.Count(), Is.EqualTo(1));
        Assert.That(db.Colectivos.Count(), Is.EqualTo(1));
    }
}
