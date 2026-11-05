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
        tarjeta.Cargar(2000);

        tarjeta.pagoRealizado(Tarjeta.TarifaBasica);

        var resultado = tarjeta.pagoRealizado(Tarjeta.TarifaBasica);

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
        tarjeta.pagoRealizado(Tarjeta.TarifaBasica);

        var resultado = tarjeta.Cargar(3000);

        Assert.That(resultado, Is.True);
        Assert.That(tarjeta.Saldo, Is.EqualTo(2850));
    }

    [Test]
    public void BoletoUsoFrecuente_Viajes1a29_PagaTarifaCompleta()
    {
        var tarjeta = new Tarjeta(1, tipoNormal);
        ViajarVeces(tarjeta, 28);

        int costoViaje29 = tarjeta.CalcularBoleto(Tarjeta.TarifaBasica);

        Assert.That(costoViaje29, Is.EqualTo(Tarjeta.TarifaBasica));
    }

    [Test]
    public void BoletoUsoFrecuente_Viajes30a79_AplicaDescuento20PorCiento()
    {
        var tarjeta = new Tarjeta(1, tipoNormal);
        ViajarVeces(tarjeta, 29);

        int costoEsperado = (int)Math.Round(Tarjeta.TarifaBasica * 0.80);
        int costoViaje30 = tarjeta.CalcularBoleto(Tarjeta.TarifaBasica);

        Assert.That(costoViaje30, Is.EqualTo(costoEsperado));
    }

    [Test]
    public void BoletoUsoFrecuente_Viajes80EnAdelante_AplicaDescuento25PorCiento()
    {
        var tarjeta = new Tarjeta(1, tipoNormal);
        ViajarVeces(tarjeta, 79);

        int costoEsperado = (int)Math.Round(Tarjeta.TarifaBasica * 0.75);
        int costoViaje80 = tarjeta.CalcularBoleto(Tarjeta.TarifaBasica);

        Assert.That(costoViaje80, Is.EqualTo(costoEsperado));
    }

    [Test]
    public void BoletoUsoFrecuente_NoAplicaATarjetasQueNoSeanNormales()
    {
        var tarjeta = new Tarjeta(2, tipoMedioBoleto);

        int costoEsperado = (int)Math.Round(Tarjeta.TarifaBasica * 0.5m);
        int costoCalculado = tarjeta.CalcularBoleto(Tarjeta.TarifaBasica);

        Assert.That(costoCalculado, Is.EqualTo(costoEsperado));
    }

    private static void ViajarVeces(Tarjeta tarjeta, int veces)
    {
        for (int i = 0; i < veces; i++)
        {
            if (tarjeta.Saldo < Tarjeta.TarifaBasica)
                tarjeta.Cargar(30000);

            tarjeta.pagoRealizado(Tarjeta.TarifaBasica);
        }
    }
}
