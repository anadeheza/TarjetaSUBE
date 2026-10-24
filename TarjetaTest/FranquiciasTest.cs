using Sube;

namespace TarjetaTest;

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

        var boleto = colectivo.PagarCon(tarjeta);

        Assert.That(boleto, Is.Not.Null, "Una tarjeta con Franquicia Completa debería poder viajar siempre.");
        Assert.That(tarjeta.Saldo, Is.EqualTo(0), "El saldo no deberia reducirse cuando tiene boleto gratuito.");
    }

    [Test]
    public void MedioBoleto()
    {
        var tarjetaNormal = new Tarjeta(1, tipoNormal);
        var tarjetaMedio = new Tarjeta(2, tipoMedioBoleto);

        int costoNormal = tarjetaNormal.CalcularBoleto(Tarjeta.TarifaBasica);
        int costoMedio = tarjetaMedio.CalcularBoleto(Tarjeta.TarifaBasica);

        Assert.That(costoNormal, Is.EqualTo(1580), "El pasaje normal deberia costar la tarifa base completa.");
        Assert.That(costoMedio, Is.EqualTo(790), "El medio boleto deberia costar exactamente la mitad.");
        Assert.That(costoMedio, Is.EqualTo(costoNormal / 2), "El costo del medio boleto debe coincidir con costoNormal / 2.");
    }
}
