using NUnit.Framework;
using Sube;

namespace TarjetaTest;

[TestFixture]
public class FranquiciaHorarioTests
{
    private TarjetaTipo tipoNormal = null!;
    private TarjetaTipo tipoMedioBoleto = null!;
    private TarjetaTipo tipoGratuito = null!;
    private TarjetaTipo tipoFranquiciaCompleta = null!;

    [SetUp]
    public void Setup()
    {
        tipoNormal = new TarjetaTipo(1, "Normal", 1.0m);
        tipoMedioBoleto = new TarjetaTipo(2, "Medio boleto estudiantil", 0.5m);
        tipoGratuito = new TarjetaTipo(3, "Boleto gratuito estudiantil", 0.0m);
        tipoFranquiciaCompleta = new TarjetaTipo(4, "Franquicia completa", 0.0m);
    }

    [Test]
    public void Franquicia_PermiteViajarEnDiaHabil_EnHorarioPermitido()
    {
        var fechaValida = new DateTime(2026, 11, 3, 10, 0, 0);
        var tarjetaMedio = new Tarjeta(1, tipoMedioBoleto);
        tarjetaMedio.Cargar(2000);
        var colectivo = new Colectivo("102");

        var boleto = colectivo.PagarCon(tarjetaMedio, fechaValida);

        Assert.That(boleto, Is.Not.Null, "Debería poder viajar en horario de franquicia.");
    }

}    