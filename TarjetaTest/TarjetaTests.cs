using Sube;

namespace TarjetaTest;

public class TarjetaTests
{
    [TestCase(2000)]
    [TestCase(3000)]
    [TestCase(4000)]
    [TestCase(5000)]
    [TestCase(8000)]
    [TestCase(10000)]
    [TestCase(15000)]
    [TestCase(20000)]
    [TestCase(25000)]
    [TestCase(30000)]
    public void Cargar_AceptaTodosLosMontosListados(int monto)
    {
        var tarjeta = new Tarjeta(1);

        var resultado = tarjeta.Cargar(monto);

        Assert.That(resultado, Is.True);
        Assert.That(tarjeta.Saldo, Is.EqualTo(monto));
    }

    [Test]
    public void Cargar_RechazaUnMontoQueNoEstaEnLaLista()
    {
        var tarjeta = new Tarjeta(1);

        var resultado = tarjeta.Cargar(1500);

        Assert.That(resultado, Is.False);
        Assert.That(tarjeta.Saldo, Is.EqualTo(0));
    }

    [Test]
    public void Cargar_RechazaSiSeSuperaElLimiteDeSaldo()
    {
        var tarjeta = new Tarjeta(1);
        tarjeta.Cargar(30000);

        var resultado = tarjeta.Cargar(15000);

        Assert.That(resultado, Is.False);
        Assert.That(tarjeta.Saldo, Is.EqualTo(30000));
    }

    [Test]
    public void Descontar_RestaElMontoSiHaySaldoSuficiente()
    {
        var tarjeta = new Tarjeta(1);
        tarjeta.Cargar(2000);

        var resultado = tarjeta.Descontar(Tarjeta.TarifaBasica);

        Assert.That(resultado, Is.True);
        Assert.That(tarjeta.Saldo, Is.EqualTo(2000 - Tarjeta.TarifaBasica));
    }

    [Test]
    public void Descontar_NoDejaSaldoNegativo()
    {
        var tarjeta = new Tarjeta(1);

        var resultado = tarjeta.Descontar(Tarjeta.TarifaBasica);

        Assert.That(resultado, Is.False);
        Assert.That(tarjeta.Saldo, Is.EqualTo(0));
    }
}
