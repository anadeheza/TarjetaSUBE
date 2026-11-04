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

    [Test]
    public void GuardaTarjetaTipoYBoletoConClaveForanea()
    {
        var opciones = new DbContextOptionsBuilder<SubeDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var db = new SubeDbContext(opciones);
        var tipo = new TarjetaTipo(1, "Normal", 1.0m);
        db.TarjetaTipos.Add(tipo);
        db.SaveChanges();

        var tarjeta = new Tarjeta(10, tipo);
        var colectivo = new Colectivo("102") { Id = 5 };
        db.Tarjetas.Add(tarjeta);
        db.Colectivos.Add(colectivo);
        db.SaveChanges();

        db.Boletos.Add(new Boleto(colectivo, tarjeta, Tarjeta.TarifaBasica));
        db.SaveChanges();

        Assert.That(db.TarjetaTipos.Count(), Is.EqualTo(1));
        Assert.That(db.Boletos.Count(), Is.EqualTo(1));
        Assert.That(db.Tarjetas.Single().Id_Tipo, Is.EqualTo(tipo.Id));
    }
}
