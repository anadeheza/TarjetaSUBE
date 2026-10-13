using NUnit.Framework;
using Sube;

namespace Sube.Tests;

[TestFixture]
public class TarjetaFranquiciaTests
{
    private TarjetaTipo tipoNormal = null!;
    private TarjetaTipo tipoMedioBoleto = null!;
    private TarjetaTipo tipoFranquiciaCompleta = null!;

    [SetUp]
    public void Setup()
    {
        tipoNormal = new TarjetaTipo(1, "Normal", 1.0m);
        tipoMedioBoleto = new TarjetaTipo(2, "Medio boleto estudiantil", 0.5m);
        tipoFranquiciaCompleta = new TarjetaTipo(4, "Franquicia completa", 0.0m);
    }

    [Test]
    public void FranquiciaCompleta()
    {
        var tarjeta = new Tarjeta(1, tipoFranquiciaCompleta);
        var colectivo = new Colectivo("102");

        bool pudoViajar = colectivo.PagarCon(tarjeta);

        Assert.IsTrue(pudoViajar, "Una tarjeta con Franquicia Completa debería poder viajar siempre.");
        Assert.AreEqual(0, tarjeta.Saldo, "El saldo no deberia reducirse cuando tiene boleto gratuito.");
    }

    [Test]
    public void MedioBoleto()
    {
        int tarifaBase = 1580;
        var tarjetaNormal = new Tarjeta(1, tipoNormal);
        var tarjetaMedio = new Tarjeta(2, tipoMedioBoleto);

        int costoNormal = tarjetaNormal.CalcularBoleto(tarifaBase);
        int costoMedio = tarjetaMedio.CalcularBoleto(tarifaBase);

        Assert.AreEqual(1580, costoNormal, "El pasaje normal deberia costar la tarifa base completa.");
        Assert.AreEqual(790, costoMedio, "El medio boleto deberia costar exactamente la mitad.");
        Assert.AreEqual(costoNormal / 2, costoMedio, "El costo del medio boleto debe coincidir con costoNormal / 2.");
    }
}