using Sube;

namespace TarjetaTest;

public class TarjetaTipoTests
{
    [Test]
    public void Constructor_GuardaIdNombreYDescuento()
    {
        var tipo = new TarjetaTipo(2, "Medio boleto estudiantil", 0.5m);

        Assert.That(tipo.Id, Is.EqualTo(2));
        Assert.That(tipo.Nombre, Is.EqualTo("Medio boleto estudiantil"));
        Assert.That(tipo.Descuento, Is.EqualTo(0.5m));
    }
}
