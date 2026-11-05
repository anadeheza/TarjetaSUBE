using NUnit.Framework;
using Sube;

namespace TarjetaTest;

[TestFixture]
public class MiBiciTuBiciTests
{
    private EstacionBicicletas estacion = null!;

    [SetUp]
    public void Setup()
    {
        estacion = new EstacionBicicletas("Estación Centro");
    }

    [Test]
    public void RetiroExitoso_TarifaCorrecta_SinDemora_ConSaldoSuficiente()
    {
        var tarjeta = new Tarjeta(1);
        tarjeta.Cargar(5000); 
        var inicio = new DateTime(2026, 11, 5, 10, 0, 0);

        bool pudoRetirar = estacion.Retirar(tarjeta, inicio);
        decimal saldoTrasRetiro = tarjeta.Saldo;

        bool pudoDevolver = estacion.Devolver(tarjeta, inicio.AddMinutes(30));

        Assert.That(pudoRetirar, Is.True, "Debería poder retirar la bici con saldo suficiente.");
        Assert.That(saldoTrasRetiro, Is.EqualTo(5000m - EstacionBicicletas.TarifaDia), "Se debió cobrar exactamente la tarifa del día.");
        Assert.That(pudoDevolver, Is.True, "Debería poder devolver la bici.");
        Assert.That(tarjeta.MultasBiciPendientes, Is.EqualTo(0m), "No debe generarse multa por devolver a tiempo.");
        Assert.That(tarjeta.TieneBiciFuera, Is.False, "La bicicleta debe figurar como devuelta.");
    }

    [Test]
    public void NoSePuedeRetirar_SinSaldoSuficiente_SinMultaPendiente()
    {
        var tarjeta = new Tarjeta(1);
        var inicio = new DateTime(2026, 11, 5, 10, 0, 0);

        bool pudoRetirar = estacion.Retirar(tarjeta, inicio);

        Assert.That(pudoRetirar, Is.False, "No debería permitir retirar sin saldo suficiente para cubrir la tarifa.");
        Assert.That(tarjeta.Saldo, Is.EqualTo(0m), "El saldo no debe sufrir modificaciones si se rechaza el retiro.");
        Assert.That(tarjeta.TieneBiciFuera, Is.False, "No se debió registrar el retiro de la bici.");
    }

    [Test]
    public void NoSePuedeRetirar_SinSaldoSuficiente_ConMultaPendiente()
    {
        var tarjeta = new Tarjeta(1);
        var inicio = new DateTime(2026, 11, 5, 10, 0, 0);

        tarjeta.Cargar(5000);
        estacion.Retirar(tarjeta, inicio);
        estacion.Devolver(tarjeta, inicio.AddHours(2)); 
        
        tarjeta.DescontarMontoFijo(2581.25m);

        bool pudoRetirar = estacion.Retirar(tarjeta, inicio.AddDays(1));

        Assert.That(pudoRetirar, Is.False, "No debe permitir retirar cuando el saldo no cubre la tarifa + multas.");
        Assert.That(tarjeta.MultasBiciPendientes, Is.EqualTo(EstacionBicicletas.Multa), "La multa debe permanecer pendiente.");
    }

    [Test]
    public void SeRetiraConUnaMultaAcumulada_CobraTarifaMasMulta()
    {
        var tarjeta = new Tarjeta(1);
        tarjeta.Cargar(10000); 
        var inicio = new DateTime(2026, 11, 5, 10, 0, 0);

        estacion.Retirar(tarjeta, inicio);
        estacion.Devolver(tarjeta, inicio.AddHours(2));

        decimal saldoAntesDeSegundoRetiro = tarjeta.Saldo;
        decimal multaEsperada = EstacionBicicletas.Multa;

        bool pudoRetirarSegundaVez = estacion.Retirar(tarjeta, inicio.AddDays(1));

        decimal cobroEsperado = EstacionBicicletas.TarifaDia + multaEsperada;
        Assert.That(pudoRetirarSegundaVez, Is.True, "Debería poder retirar al tener saldo para cubrir tarifa + multa.");
        Assert.That(tarjeta.Saldo, Is.EqualTo(saldoAntesDeSegundoRetiro - cobroEsperado), "Se debe descontar la tarifa del día MÁS la multa acumulada.");
        Assert.That(tarjeta.MultasBiciPendientes, Is.EqualTo(0m), "Las multas pendientes deben quedar saldadas a $0.");
    }

    [Test]
    public void SeRetiraConVariasMultasAcumuladasElMismoDia()
    {
        var tarjeta = new Tarjeta(1);
        tarjeta.Cargar(20000);
        var inicio = new DateTime(2026, 11, 5, 8, 0, 0);

        estacion.Retirar(tarjeta, inicio);
        estacion.Devolver(tarjeta, inicio.AddHours(2));
        
        var segundoInicio = inicio.AddHours(3);
        estacion.Retirar(tarjeta, segundoInicio);
        estacion.Devolver(tarjeta, segundoInicio.AddHours(2));

        Assert.That(tarjeta.MultasBiciPendientes, Is.EqualTo(EstacionBicicletas.Multa * 2), "Deben haberse acumulado 2 multas.");

        decimal saldoAntesDeTercerRetiro = tarjeta.Saldo;
        decimal totalMultasAcumuladas = EstacionBicicletas.Multa * 2;

        bool pudoRetirarTerceraVez = estacion.Retirar(tarjeta, inicio.AddHours(6));

        decimal cobroTotalEsperado = EstacionBicicletas.TarifaDia + totalMultasAcumuladas;
        Assert.That(pudoRetirarTerceraVez, Is.True, "Debe permitir retirar si tiene saldo para tarifa + todas las multas.");
        Assert.That(tarjeta.Saldo, Is.EqualTo(saldoAntesDeTercerRetiro - cobroTotalEsperado), "Debe cobrar la tarifa y la suma de todas las multas acumuladas.");
        Assert.That(tarjeta.MultasBiciPendientes, Is.EqualTo(0m), "Todas las multas deben quedar canceladas tras el retiro.");
    }
}