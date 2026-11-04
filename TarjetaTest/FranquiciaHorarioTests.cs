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
        //Martes 10:00
        var fechaValida = new DateTime(2026, 11, 3, 10, 0, 0);
        var tarjetaMedio = new Tarjeta(1, tipoMedioBoleto);
        tarjetaMedio.Cargar(2000);
        var colectivo = new Colectivo("102");

        var boleto = colectivo.PagarCon(tarjetaMedio, fechaValida);

        Assert.That(boleto, Is.Not.Null, "Debería poder viajar en horario de franquicia.");
    }


    [Test]
    public void Franquicia_RechazaViajeAntesDeLas6AM()
    {
        // Martes 05:59
        var fechaTemprano = new DateTime(2026, 11, 3, 5, 59, 0);
        var tarjetaGratuita = new Tarjeta(1, tipoGratuito);
        var colectivo = new Colectivo("102");

        var boleto = colectivo.PagarCon(tarjetaGratuita, fechaTemprano);

        Assert.That(boleto, Is.Null, "No debería permitir viajar antes de las 6 AM.");
    }

    [Test]
    public void Franquicia_RechazaViajeAComenzarOdespuesDeLas22()
    {
        // Martes 22:00
        var fechaNoche = new DateTime(2026, 11, 3, 22, 0, 0);
        var tarjetaFranquicia = new Tarjeta(1, tipoFranquiciaCompleta);
        var colectivo = new Colectivo("102");

        var boleto = colectivo.PagarCon(tarjetaFranquicia, fechaNoche);

        Assert.That(boleto, Is.Null, "No debería permitir viajar a partir de las 22:00 hs.");
    }

    [Test]
    public void Franquicia_RechazaViajeFinDeSemana()
    {
        // Sábado 12:00
        var sabado = new DateTime(2026, 11, 7, 12, 0, 0);
        // Domingo 12:00
        var domingo = new DateTime(2026, 11, 8, 12, 0, 0);

        var tarjetaMedio = new Tarjeta(1, tipoMedioBoleto);
        tarjetaMedio.Cargar(2000);
        var colectivo = new Colectivo("102");

        var boletoSabado = colectivo.PagarCon(tarjetaMedio, sabado);
        var boletoDomingo = colectivo.PagarCon(tarjetaMedio, domingo);

        Assert.That(boletoSabado, Is.Null, "No debería permitir franquicias en día sábado.");
        Assert.That(boletoDomingo, Is.Null, "No debería permitir franquicias en día domingo.");
    }

    [Test]
    public void TarjetaNormal_PuedeViajarFueraDeHorarioYFinDeSemana()
    {
        // Sábado 23:00
        var sabadoNoche = new DateTime(2026, 11, 7, 23, 0, 0);
        var tarjetaNormal = new Tarjeta(1, tipoNormal);
        tarjetaNormal.Cargar(3000);
        var colectivo = new Colectivo("102");

        var boleto = colectivo.PagarCon(tarjetaNormal, sabadoNoche);

        Assert.That(boleto, Is.Not.Null, "La tarjeta normal no tiene restricciones de horario ni días.");
    }
}    