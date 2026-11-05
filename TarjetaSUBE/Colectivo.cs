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

        bool esTrasbordo = tarjeta.EsTrasbordo(Linea, ahora);
        decimal saldoAnterior = tarjeta.Saldo;
        int precio = tarjeta.CalcularBoleto(Tarjeta.TarifaBasica, Linea, ahora);

        if (!tarjeta.pagoRealizado(Tarjeta.TarifaBasica, Linea, ahora))
            return null;

        int totalAbonado = precio;
        if (saldoAnterior < 0 && precio > 0)
            totalAbonado = precio - (int)saldoAnterior;

        return new Boleto(colectivo: this, tarjeta: tarjeta, precio: precio, totalAbonado: totalAbonado, saldoRestante: (int)tarjeta.Saldo, fecha: ahora, esTrasbordo: esTrasbordo);
    }
}