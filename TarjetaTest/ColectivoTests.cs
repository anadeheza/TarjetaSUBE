using Sube;

namespace TarjetaTest;

public class ColectivoTests
{
    [Test]
    public void PagarCon_GeneraBoletoYDescuentaSaldo()
    {
        var tarjeta = new Tarjeta(1);
        tarjeta.Cargar(2000);
        var colectivo = new Colectivo("102");

        var boleto = colectivo.PagarCon(tarjeta);

        Assert.That(boleto, Is.Not.Null);
        Assert.That(boleto!.Precio, Is.EqualTo(Tarjeta.TarifaBasica));
        Assert.That(tarjeta.saldo, Is.EqualTo(2000 - Tarjeta.TarifaBasica));
    }

    [Test]
    public void PagarCon_NoViajaSiElSaldoNoAlcanza()
    {
        var tarjeta = new Tarjeta(1);
        var colectivo = new Colectivo("102");

        var boleto = colectivo.PagarCon(tarjeta);

        Assert.That(boleto, Is.Null);
        Assert.That(tarjeta.saldo, Is.EqualTo(0));
    }
}
