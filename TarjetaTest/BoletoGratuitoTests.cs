using Sube;

namespace TarjetaTest;

public class BoletoGratuitoTests
{
    private TarjetaTipo tipoGratuito = null!;
    private TarjetaTipo tipoFranquiciaCompleta = null!;

    [SetUp]
    public void Setup()
    {
        tipoGratuito = new TarjetaTipo(3, "Boleto gratuito estudiantil", 0.0m);
        tipoFranquiciaCompleta = new TarjetaTipo(4, "Franquicia completa", 0.0m);
    }

    [Test]
    public void BoletoGratuito_SoloDosViajesGratisPorDia()
    {
        var tarjeta = new Tarjeta(1, tipoGratuito);
        tarjeta.Cargar(5000);
        var colectivo = new Colectivo("102");
        var inicio = new DateTime(2026, 11, 3, 10, 0, 0);

        var primero = colectivo.PagarCon(tarjeta, inicio);
        var segundo = colectivo.PagarCon(tarjeta, inicio.AddMinutes(1));
        var tercero = colectivo.PagarCon(tarjeta, inicio.AddMinutes(2));

        Assert.That(primero, Is.Not.Null);
        Assert.That(segundo, Is.Not.Null);
        Assert.That(tercero, Is.Not.Null, "El tercer viaje se tiene que poder hacer.");
        Assert.That(primero!.GetTotalAbonado(), Is.EqualTo(0));
        Assert.That(segundo!.GetTotalAbonado(), Is.EqualTo(0));
        Assert.That(tercero!.GetTotalAbonado(), Is.EqualTo(Tarjeta.TarifaBasica), "A partir del tercero se cobra tarifa completa.");
        Assert.That(tarjeta.Saldo, Is.EqualTo(5000 - Tarjeta.TarifaBasica));
    }

    [Test]
    public void BoletoGratuito_AlDiaSiguienteVuelveASerGratis()
    {
        var tarjeta = new Tarjeta(1, tipoGratuito);
        tarjeta.Cargar(5000);
        var colectivo = new Colectivo("102");
        var inicio = new DateTime(2026, 11, 3, 10, 0, 0);

        colectivo.PagarCon(tarjeta, inicio);
        colectivo.PagarCon(tarjeta, inicio.AddMinutes(1));
        colectivo.PagarCon(tarjeta, inicio.AddMinutes(2));

        var alDiaSiguiente = colectivo.PagarCon(tarjeta, inicio.AddDays(1));

        Assert.That(alDiaSiguiente, Is.Not.Null);
        Assert.That(alDiaSiguiente!.GetTotalAbonado(), Is.EqualTo(0));
    }

    [Test]
    public void FranquiciaCompleta_SigueViajandoGratisSinTopeDiario()
    {
        var tarjeta = new Tarjeta(1, tipoFranquiciaCompleta);
        var colectivo = new Colectivo("102");
        var inicio = new DateTime(2026, 11, 3, 10, 0, 0);

        var primero = colectivo.PagarCon(tarjeta, inicio);
        var segundo = colectivo.PagarCon(tarjeta, inicio.AddMinutes(1));
        var tercero = colectivo.PagarCon(tarjeta, inicio.AddMinutes(2));

        Assert.That(primero, Is.Not.Null);
        Assert.That(segundo, Is.Not.Null);
        Assert.That(tercero, Is.Not.Null);
        Assert.That(primero!.GetTotalAbonado(), Is.EqualTo(0));
        Assert.That(segundo!.GetTotalAbonado(), Is.EqualTo(0));
        Assert.That(tercero!.GetTotalAbonado(), Is.EqualTo(0));
        Assert.That(tarjeta.Saldo, Is.EqualTo(0));
    }
}