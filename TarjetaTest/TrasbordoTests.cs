using NUnit.Framework;
using Sube;

namespace TarjetaTest;

[TestFixture]
public class TrasbordoTests
{
    private TarjetaTipo tipoNormal = null!;
    private TarjetaTipo tipoMedio = null!;

    [SetUp]
    public void Setup()
    {
        tipoNormal = new TarjetaTipo(1, "Normal", 1.0m);
        tipoMedio = new TarjetaTipo(2, "Medio boleto estudiantil", 0.5m);
    }

    [Test]
    public void Trasbordo_DentroDeLaHoraYDistintaLinea_CobraCeroYMarcaBoleto()
    {
        var tarjeta = new Tarjeta(1, tipoNormal);
        tarjeta.Cargar(5000);

        var colectivo1 = new Colectivo("102");
        var colectivo2 = new Colectivo("115");

        //lunes a las 10:00
        var horaPrimerViaje = new DateTime(2026, 3, 2, 10, 0, 0); 
        var horaSegundoViaje = new DateTime(2026, 3, 2, 10, 30, 0); 

        var boleto1 = colectivo1.PagarCon(tarjeta, horaPrimerViaje);
        Assert.That(boleto1, Is.Not.Null);
        Assert.That(boleto1!.Precio, Is.EqualTo(Tarjeta.TarifaBasica));
        Assert.That(boleto1.EsTrasbordo, Is.False);

        var boleto2 = colectivo2.PagarCon(tarjeta, horaSegundoViaje);
        Assert.That(boleto2, Is.Not.Null);
        Assert.That(boleto2!.Precio, Is.EqualTo(0));
        Assert.That(boleto2.EsTrasbordo, Is.True);
    }

    [Test]
    public void Trasbordo_MismaLinea_NoEsGratis()
    {
        var tarjeta = new Tarjeta(1, tipoNormal);
        tarjeta.Cargar(5000);

        var colectivo1 = new Colectivo("102");
        var colectivo2 = new Colectivo("102");

        var hora1 = new DateTime(2026, 3, 2, 10, 0, 0);
        var hora2 = new DateTime(2026, 3, 2, 10, 20, 0);

        colectivo1.PagarCon(tarjeta, hora1);
        var boleto2 = colectivo2.PagarCon(tarjeta, hora2);

        Assert.That(boleto2!.Precio, Is.EqualTo(Tarjeta.TarifaBasica));
        Assert.That(boleto2.EsTrasbordo, Is.False);
    }

    [Test]
    public void Trasbordo_FueraDeHorarioOPasoMasDeUnaHora_NoEsGratis()
    {
        var tarjeta = new Tarjeta(1, tipoNormal);
        tarjeta.Cargar(5000);

        var colectivo1 = new Colectivo("102");
        var colectivo2 = new Colectivo("115");

        //70 minutos
        var hora1 = new DateTime(2026, 3, 2, 10, 0, 0);
        var hora2 = new DateTime(2026, 3, 2, 11, 10, 0);

        colectivo1.PagarCon(tarjeta, hora1);
        var boleto2 = colectivo2.PagarCon(tarjeta, hora2);

        Assert.That(boleto2!.Precio, Is.EqualTo(Tarjeta.TarifaBasica));
        Assert.That(boleto2.EsTrasbordo, Is.False);
    }

    [Test]
    public void Trasbordo_Domingo_NoAplica()
    {
        var tarjeta = new Tarjeta(1, tipoNormal);
        tarjeta.Cargar(5000);

        var colectivo1 = new Colectivo("102");
        var colectivo2 = new Colectivo("115");

        //domingo a las 10:00
        var hora1 = new DateTime(2026, 11, 8, 10, 0, 0);
        var hora2 = new DateTime(2026, 11, 8, 10, 20, 0);

        colectivo1.PagarCon(tarjeta, hora1);
        var boleto2 = colectivo2.PagarCon(tarjeta, hora2);

        Assert.That(boleto2!.Precio, Is.EqualTo(Tarjeta.TarifaBasica));
        Assert.That(boleto2.EsTrasbordo, Is.False);
    }
}