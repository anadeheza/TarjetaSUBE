using Sube;

namespace TarjetaTest;

public class TarjetaTests
{
    private TarjetaTipo tipoNormal = null!;
    private TarjetaTipo tipoMedioBoleto = null!;

    [SetUp]
    public void Setup()
    {
        tipoNormal = new TarjetaTipo(1, "Normal", 1.0m);
        tipoMedioBoleto = new TarjetaTipo(2, "Medio boleto estudiantil", 0.5m);
    }

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
        Assert.That(tarjeta.saldo, Is.EqualTo(monto));
    }

    [Test]
    public void Cargar_RechazaUnMontoQueNoEstaEnLaLista()
    {
        var tarjeta = new Tarjeta(1);

        var resultado = tarjeta.Cargar(1500);

        Assert.That(resultado, Is.False);
        Assert.That(tarjeta.saldo, Is.EqualTo(0));
    }

    [Test]
    public void Cargar_RechazaSiSeSuperaElLimiteDeSaldo()
    {
        var tarjeta = new Tarjeta(1);
        tarjeta.Cargar(30000);

        var resultado = tarjeta.Cargar(15000);

        Assert.That(resultado, Is.False);
        Assert.That(tarjeta.saldo, Is.EqualTo(30000));
    }

    [Test]
    public void Descontar_RestaElMontoSiHaySaldoSuficiente()
    {
        var tarjeta = new Tarjeta(1);
        tarjeta.Cargar(2000);

        var resultado = tarjeta.pagoRealizado(Tarjeta.TarifaBasica);

        Assert.That(resultado, Is.True);
        Assert.That(tarjeta.saldo, Is.EqualTo(2000 - Tarjeta.TarifaBasica));
    }

    [Test]
    public void Descontar_PermiteViajePlusSiNoHaySaldo()
    {
        var tarjeta = new Tarjeta(1);

        var resultado = tarjeta.pagoRealizado(Tarjeta.TarifaBasica);

        Assert.That(resultado, Is.True);
        Assert.That(tarjeta.Saldo, Is.EqualTo(-Tarjeta.TarifaBasica));
    }

    [Test]
    public void Descontar_NoPermiteBajarDelSaldoMinimo()
    {
        var tarjeta = new Tarjeta(1);
        tarjeta.pagoRealizado(Tarjeta.TarifaBasica);

        var resultado = tarjeta.pagoRealizado(Tarjeta.TarifaBasica);

        Assert.That(resultado, Is.False);
        Assert.That(tarjeta.Saldo, Is.EqualTo(-Tarjeta.TarifaBasica));
    }

    [Test]
    public void Cargar_DescuentaElViajePlus() 
    {
        var tarjeta = new Tarjeta(1);
        tarjeta.pagoRealizado(Tarjeta.TarifaBasica);

        var resultado = tarjeta.Cargar(2000);

        Assert.That(resultado, Is.True);
        Assert.That(tarjeta.Saldo, Is.EqualTo(2000 - Tarjeta.TarifaBasica));
    }

    [Test]
    public void BoletoUsoFrecuente_Viajes1a29_PagaTarifaCompleta()
    {
        var tarjeta = new Tarjeta(1, tipoNormal);

        for (int i = 0; i < 28; i++)
        {
            tarjeta.pagoRealizado(Tarjeta.TarifaBasica);
        }

        int costoViaje29 = tarjeta.CalcularBoleto(Tarjeta.TarifaBasica);

        Assert.That(costoViaje29, Is.EqualTo(Tarjeta.TarifaBasica));
    }

    [Test]
    public void BoletoUsoFrecuente_Viajes30a79_AplicaDescuento20PorCiento()
    {
        var tarjeta = new Tarjeta(1, tipoNormal);

        for (int i = 0; i < 29; i++)
        {
            tarjeta.pagoRealizado(Tarjeta.TarifaBasica);
        }

        int costoEsperado = (int)Math.Round(Tarjeta.TarifaBasica * 0.80);
        int costoViaje30 = tarjeta.CalcularBoleto(Tarjeta.TarifaBasica);

        Assert.That(costoViaje30, Is.EqualTo(costoEsperado));
    }

    [Test]
    public void BoletoUsoFrecuente_Viajes80EnAdelante_AplicaDescuento25PorCiento()
    {
        var tarjeta = new Tarjeta(1, tipoNormal);

        for (int i = 0; i < 79; i++)
        {
            tarjeta.pagoRealizado(Tarjeta.TarifaBasica);
        }

        int costoEsperado = (int)Math.Round(Tarjeta.TarifaBasica * 0.75);
        int costoViaje80 = tarjeta.CalcularBoleto(Tarjeta.TarifaBasica);

        Assert.That(costoViaje80, Is.EqualTo(costoEsperado));
    }

    [Test]
    public void BoletoUsoFrecuente_NoAplicaATarjetasQueNoSeanNormales()
    {
        var tarjeta = new Tarjeta(2, tipoMedioBoleto);

        for (int i = 0; i < 35; i++)
        {
            tarjeta.pagoRealizado(Tarjeta.TarifaBasica);
        }

        int costoEsperado = (int)Math.Round(Tarjeta.TarifaBasica * 0.5m);
        int costoCalculado = tarjeta.CalcularBoleto(Tarjeta.TarifaBasica);

        Assert.That(costoCalculado, Is.EqualTo(costoEsperado));
    }
}