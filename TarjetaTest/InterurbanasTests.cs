using NUnit.Framework;
using Sube;

namespace TarjetaTest;

[TestFixture]
public class InterurbanoTests
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
    public void LineaInterurbana_TarjetaNormal_CobraTarifa3000()
    {
        var tarjeta = new Tarjeta(1, tipoNormal);
        tarjeta.Cargar(5000);

        var colectivoInterurbano = new ColectivoInterurbano("35/9 Baigorria");
        var boleto = colectivoInterurbano.PagarCon(tarjeta);

        Assert.That(boleto, Is.Not.Null);
        Assert.That(boleto!.Precio, Is.EqualTo(3000));
        Assert.That(tarjeta.Saldo, Is.EqualTo(5000 - 3000));
    }

    [Test]
    public void LineaInterurbana_MedioBoleto_AplicaDescuentoSobreTarifa3000()
    {
        var tarjeta = new Tarjeta(2, tipoMedio);
        tarjeta.Cargar(5000);

        var colectivoInterurbano = new ColectivoInterurbano("Expreso Gálvez");
        
        var fechaHabil = new DateTime(2026, 3, 2, 10, 0, 0); 
        var boleto = colectivoInterurbano.PagarCon(tarjeta, fechaHabil);

        Assert.That(boleto, Is.Not.Null);
        Assert.That(boleto!.Precio, Is.EqualTo(1500));
    }
}