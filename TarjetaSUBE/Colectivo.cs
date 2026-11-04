namespace Sube;

public class Colectivo
{
    public int Id { get; set; }
    public string Linea { get; set; } = string.Empty;

    public Colectivo() { }

    public Colectivo(string linea)
    {
        Linea = linea;
    }

    public Boleto? PagarCon(Tarjeta tarjeta, DateTime? fecha = null)
    {
        var ahora = fecha ?? DateTime.Now;

        if (!tarjeta.PuedeViajar(ahora))
            return null;

        tarjeta.ActualizarDia(ahora);

        int saldoAnterior = tarjeta.Saldo;
        int precio = tarjeta.CalcularBoleto(Tarjeta.TarifaBasica);

        if (!tarjeta.pagoRealizado(Tarjeta.TarifaBasica, ahora))
            return null;

        int totalAbonado = precio;
        if (saldoAnterior < 0)
            totalAbonado = precio - saldoAnterior;

        return new Boleto(this, tarjeta, precio, totalAbonado, tarjeta.Saldo, ahora);
    }
}
