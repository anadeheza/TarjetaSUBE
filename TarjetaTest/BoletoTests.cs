using Sube;

namespace TarjetaTest;

public class BoletoTests
{
    private TarjetaTipo tipoNormal = null!;
    private TarjetaTipo tipoMedioBoleto = null!;
    private TarjetaTipo tipoGratuitoEstudiantil = null!;
    private TarjetaTipo tipoFranquiciaCompleta = null!;

    [SetUp]
    public void Setup()
    {
        tipoNormal = new TarjetaTipo(1, "Normal", 1.0m);
        tipoMedioBoleto = new TarjetaTipo(2, "Medio boleto estudiantil", 0.5m);
        tipoGratuitoEstudiantil = new TarjetaTipo(3, "Boleto gratuito estudiantil", 0.0m);
        tipoFranquiciaCompleta = new TarjetaTipo(4, "Franquicia completa", 0.0m);
    }

    [Test]
    public void Constructor_GuardaPrecioTarjetaYColectivo()
    {
        var tarjeta = new Tarjeta(7);
        var colectivo = new Colectivo("120");

        var boleto = new Boleto(colectivo, tarjeta, Tarjeta.TarifaBasica);

        Assert.That(boleto.Precio, Is.EqualTo(Tarjeta.TarifaBasica));
        Assert.That(boleto.TarjetaId, Is.EqualTo(7));
        Assert.That(boleto.Colectivo, Is.EqualTo(colectivo));
        Assert.That(boleto.Tarjeta, Is.EqualTo(tarjeta));
    }

    [Test]
    public void BoletoNormal_InformaTodosLosDatos()
    {
        var tarjeta = new Tarjeta(12, tipoNormal);
        tarjeta.Cargar(2000);
        var colectivo = new Colectivo("102");
        var antes = DateTime.Now;

        var boleto = colectivo.PagarCon(tarjeta);

        Assert.That(boleto, Is.Not.Null);
        Assert.That(boleto!.GetFecha(), Is.InRange(antes, DateTime.Now));
        Assert.That(boleto.GetTipoTarjeta(), Is.EqualTo("Normal"));
        Assert.That(boleto.GetLinea(), Is.EqualTo("102"));
        Assert.That(boleto.GetTotalAbonado(), Is.EqualTo(Tarjeta.TarifaBasica));
        Assert.That(boleto.GetIdTarjeta(), Is.EqualTo(12));
        Assert.That(boleto.GetSaldoRestante(), Is.EqualTo(2000 - Tarjeta.TarifaBasica));
        Assert.That(boleto.GetSaldo(), Is.EqualTo(boleto.GetSaldoRestante()));
    }

    [Test]
    public void BoletoMedioBoleto_AbonaLaMitad()
    {
        var tarjeta = new Tarjeta(2, tipoMedioBoleto);
        tarjeta.Cargar(2000);
        var colectivo = new Colectivo("132");

        var boleto = colectivo.PagarCon(tarjeta);

        Assert.That(boleto, Is.Not.Null);
        Assert.That(boleto!.GetTipoTarjeta(), Is.EqualTo("Medio boleto estudiantil"));
        Assert.That(boleto.GetLinea(), Is.EqualTo("132"));
        Assert.That(boleto.GetTotalAbonado(), Is.EqualTo(Tarjeta.TarifaBasica / 2));
        Assert.That(boleto.GetIdTarjeta(), Is.EqualTo(2));
        Assert.That(boleto.GetSaldoRestante(), Is.EqualTo(2000 - Tarjeta.TarifaBasica / 2));
    }

    [Test]
    public void BoletoGratuitoEstudiantil_NoAbonaNada()
    {
        var tarjeta = new Tarjeta(3, tipoGratuitoEstudiantil);
        var colectivo = new Colectivo("120");

        var boleto = colectivo.PagarCon(tarjeta);

        Assert.That(boleto, Is.Not.Null);
        Assert.That(boleto!.GetTipoTarjeta(), Is.EqualTo("Boleto gratuito estudiantil"));
        Assert.That(boleto.GetLinea(), Is.EqualTo("120"));
        Assert.That(boleto.GetTotalAbonado(), Is.EqualTo(0));
        Assert.That(boleto.GetIdTarjeta(), Is.EqualTo(3));
        Assert.That(boleto.GetSaldoRestante(), Is.EqualTo(0));
    }

    [Test]
    public void BoletoFranquiciaCompleta_NoAbonaNada()
    {
        var tarjeta = new Tarjeta(4, tipoFranquiciaCompleta);
        var colectivo = new Colectivo("112");

        var boleto = colectivo.PagarCon(tarjeta);

        Assert.That(boleto, Is.Not.Null);
        Assert.That(boleto!.GetTipoTarjeta(), Is.EqualTo("Franquicia completa"));
        Assert.That(boleto.GetLinea(), Is.EqualTo("112"));
        Assert.That(boleto.GetTotalAbonado(), Is.EqualTo(0));
        Assert.That(boleto.GetIdTarjeta(), Is.EqualTo(4));
        Assert.That(boleto.GetSaldoRestante(), Is.EqualTo(0));
    }

    [Test]
    public void Boleto_ConSaldoNegativo_AbonaMasQueLaTarifa()
    {
        var tarjeta = new Tarjeta(8, tipoNormal);
        tarjeta.saldo = -200;
        var colectivo = new Colectivo("102");

        var boleto = colectivo.PagarCon(tarjeta);

        Assert.That(boleto, Is.Not.Null);
        Assert.That(boleto!.GetTotalAbonado(), Is.EqualTo(Tarjeta.TarifaBasica + 200));
        Assert.That(boleto.GetTotalAbonado(), Is.GreaterThan(Tarjeta.TarifaBasica));
        Assert.That(boleto.GetSaldoRestante(), Is.EqualTo(-200 - Tarjeta.TarifaBasica));
        Assert.That(boleto.GetTipoTarjeta(), Is.EqualTo("Normal"));
        Assert.That(boleto.GetIdTarjeta(), Is.EqualTo(8));
    }
}
