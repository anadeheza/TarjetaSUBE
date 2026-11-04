using Sube;

namespace TarjetaTest;

public class MedioBoletoTests
{
    private TarjetaTipo tipoNormal = null!;
    private TarjetaTipo tipoMedioBoleto = null!;

    [SetUp]
    public void Setup()
    {
        tipoNormal = new TarjetaTipo(1, "Normal", 1.0m);
        tipoMedioBoleto = new TarjetaTipo(2, "Medio boleto estudiantil", 0.5m);
    }

    [Test]
    public void MedioBoleto_RechazaOtroViajeAntesDe5Minutos()
    {
        var tarjeta = new Tarjeta(1, tipoMedioBoleto);
        tarjeta.Cargar(5000);
        var colectivo = new Colectivo("102");
        var inicio = new DateTime(2026, 11, 3, 10, 0, 0);

        var primero = colectivo.PagarCon(tarjeta, inicio);
        int saldoDespuesDelPrimero = tarjeta.Saldo;

        var segundo = colectivo.PagarCon(tarjeta, inicio.AddMinutes(4));

        Assert.That(primero, Is.Not.Null);
        Assert.That(segundo, Is.Null, "No debería poder marcar de nuevo antes de los 5 minutos.");
        Assert.That(tarjeta.Saldo, Is.EqualTo(saldoDespuesDelPrimero), "Si se rechaza, el saldo no tiene que cambiar.");
    }

    [Test]
    public void MedioBoleto_PermiteOtroViajeALos5Minutos()
    {
        var tarjeta = new Tarjeta(1, tipoMedioBoleto);
        tarjeta.Cargar(5000);
        var colectivo = new Colectivo("102");
        var inicio = new DateTime(2026, 11, 3, 10, 0, 0);

        var primero = colectivo.PagarCon(tarjeta, inicio);
        var segundo = colectivo.PagarCon(tarjeta, inicio.AddMinutes(5));

        Assert.That(primero, Is.Not.Null);
        Assert.That(segundo, Is.Not.Null, "A los 5 minutos exactos ya debería poder viajar.");
    }

    [Test]
    public void TarjetaNormal_PuedeViajarDosVecesSeguidas()
    {
        var tarjeta = new Tarjeta(1, tipoNormal);
        tarjeta.Cargar(5000);
        var colectivo = new Colectivo("102");
        var inicio = new DateTime(2026, 11, 3, 10, 0, 0);

        var primero = colectivo.PagarCon(tarjeta, inicio);
        var segundo = colectivo.PagarCon(tarjeta, inicio.AddMinutes(1));

        Assert.That(primero, Is.Not.Null);
        Assert.That(segundo, Is.Not.Null, "La espera de 5 minutos solo aplica a medio boleto.");
    }

    [Test]
    public void MedioBoleto_ElTercerViajeDelDiaSaleTarifaNormal()
    {
        var tarjeta = new Tarjeta(1, tipoMedioBoleto);
        tarjeta.Cargar(5000);
        var colectivo = new Colectivo("102");
        var inicio = new DateTime(2026, 11, 3, 10, 0, 0);

        var primero = colectivo.PagarCon(tarjeta, inicio);
        var segundo = colectivo.PagarCon(tarjeta, inicio.AddMinutes(5));
        var tercero = colectivo.PagarCon(tarjeta, inicio.AddMinutes(10));

        Assert.That(primero, Is.Not.Null);
        Assert.That(segundo, Is.Not.Null);
        Assert.That(tercero, Is.Not.Null, "El tercer viaje se tiene que poder hacer.");
        Assert.That(primero!.GetTotalAbonado(), Is.EqualTo(Tarjeta.TarifaBasica / 2));
        Assert.That(segundo!.GetTotalAbonado(), Is.EqualTo(Tarjeta.TarifaBasica / 2));
        Assert.That(tercero!.GetTotalAbonado(), Is.EqualTo(Tarjeta.TarifaBasica), "El tercer viaje ya no es medio boleto.");
    }

    [Test]
    public void MedioBoleto_AlDiaSiguienteVuelveACobrarMedio()
    {
        var tarjeta = new Tarjeta(1, tipoMedioBoleto);
        tarjeta.Cargar(5000);
        var colectivo = new Colectivo("102");
        var inicio = new DateTime(2026, 11, 3, 10, 0, 0);

        colectivo.PagarCon(tarjeta, inicio);
        colectivo.PagarCon(tarjeta, inicio.AddMinutes(5));
        colectivo.PagarCon(tarjeta, inicio.AddMinutes(10));

        var alDiaSiguiente = colectivo.PagarCon(tarjeta, inicio.AddDays(1));

        Assert.That(alDiaSiguiente, Is.Not.Null);
        Assert.That(alDiaSiguiente!.GetTotalAbonado(), Is.EqualTo(Tarjeta.TarifaBasica / 2));
    }
}
