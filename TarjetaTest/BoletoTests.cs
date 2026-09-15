using Sube;

namespace TarjetaTest;

public class BoletoTests
{
    [Test]
    public void Constructor_GuardaPrecioTarjetaYColectivo()
    {
        var tarjeta = new Tarjeta(7);
        var colectivo = new Colectivo("120");

        var boleto = new Boleto(colectivo, tarjeta, Tarjeta.TarifaBasica);

        Assert.That(boleto.Precio, Is.EqualTo(Tarjeta.TarifaBasica));
        Assert.That(boleto.TarjetaId, Is.EqualTo(7));
        Assert.That(boleto.Colectivo, Is.EqualTo(colectivo));
        Assert.That(boleto.Tarjeta, Is.EqualTo(tarjeta));
    }
}
