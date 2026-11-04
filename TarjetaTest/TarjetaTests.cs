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
    public void Cargar_AceptaUnaCargaQueAntesSuperabaElTopeViejo()
    {
        var tarjeta = new Tarjeta(1);
        tarjeta.Cargar(30000);

        var resultado = tarjeta.Cargar(15000);

        Assert.That(resultado, Is.True);
        Assert.That(tarjeta.Saldo, Is.EqualTo(45000));
        Assert.That(tarjeta.SaldoPendiente, Is.EqualTo(0));
    }

    [Test]
    public void Descontar_RestaElMontoSiHaySaldoSuficiente()
    {
        var tarjeta = new Tarjeta(1);
        tarjeta.Cargar(3000);

        var resultado = tarjeta.pagoRealizado(Tarjeta.TarifaBasica);

        Assert.That(resultado, Is.True);
        Assert.That(tarjeta.Saldo, Is.EqualTo(3000 - Tarjeta.TarifaBasica));
    }

    [Test]
    public void Descontar_PermiteViajePlusSiNoHaySaldo()
    {
        var tarjeta = new Tarjeta(1);
        tarjeta.Cargar(2000); //minimo de carga

        tarjeta.pagoRealizado(Tarjeta.TarifaBasica); //cuesta 2150, saldo queda en -150

        var resultado = tarjeta.pagoRealizado(Tarjeta.TarifaBasica); //me resta 2150 mas, si se cobra quedaria en -2300 que supera el limite de -2000, tiene que ser rechazado

        Assert.That(resultado, Is.False);
        Assert.That(tarjeta.Saldo, Is.EqualTo(-150));
    }

    [Test]
    public void Descontar_NoPermiteBajarDelSaldoMinimo()
    {
        var tarjeta = new Tarjeta(1);

        bool resultado = tarjeta.pagoRealizado(Tarjeta.TarifaBasica);

        Assert.That(resultado, Is.False);
        Assert.That(tarjeta.Saldo, Is.EqualTo(0));
    }

    [Test]
    public void Cargar_DescuentaElViajePlus() 
    {
        var tarjeta = new Tarjeta(1);
        tarjeta.Cargar(2000);
        tarjeta.pagoRealizado(Tarjeta.TarifaBasica); //saldo -150

        var resultado = tarjeta.Cargar(3000); //3000 - 150 = 2850

        Assert.That(resultado, Is.True);
        Assert.That(tarjeta.Saldo, Is.EqualTo(2850));
    }
}
