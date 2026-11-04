using Sube;

namespace TarjetaTest;

public class SaldoPendienteTests
{
    [Test]
    public void Cargar_SiSuperaElMaximo_DejaElExcedentePendiente()
    {
        var tarjeta = new Tarjeta(1);

        var primera = tarjeta.Cargar(30000);
        var segunda = tarjeta.Cargar(30000);

        Assert.That(primera, Is.True);
        Assert.That(segunda, Is.True);
        Assert.That(tarjeta.Saldo, Is.EqualTo(Tarjeta.SaldoMax));
        Assert.That(tarjeta.SaldoPendiente, Is.EqualTo(4000));
    }

    [Test]
    public void PagarCon_ConSaldoPendiente_AcreditaHastaElMaximo()
    {
        var tarjeta = new Tarjeta(1);
        tarjeta.Cargar(30000);
        tarjeta.Cargar(30000);
        var colectivo = new Colectivo("102");

        var boleto = colectivo.PagarCon(tarjeta);

        Assert.That(boleto, Is.Not.Null);
        Assert.That(tarjeta.Saldo, Is.EqualTo(Tarjeta.SaldoMax));
        Assert.That(tarjeta.SaldoPendiente, Is.EqualTo(4000 - Tarjeta.TarifaBasica));
    }
}
