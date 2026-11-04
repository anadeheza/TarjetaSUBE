namespace Sube;

public class Boleto
{
    public int Id { get; set; }
    public int Precio { get; set; }
    public DateTime Fecha { get; set; }
    public string TipoTarjeta { get; set; } = "Normal";
    public string Linea { get; set; } = string.Empty;
    public int TotalAbonado { get; set; }
    public int SaldoRestante { get; set; }

    public int TarjetaId { get; set; }
    public Tarjeta Tarjeta { get; set; } = null!;

    public int ColectivoId { get; set; }
    public Colectivo Colectivo { get; set; } = null!;

    public Boleto() { }

    public Boleto(Colectivo colectivo, Tarjeta tarjeta, int precio, int? totalAbonado = null, int? saldoRestante = null, DateTime? fecha = null)
    {
        Colectivo = colectivo;
        Tarjeta = tarjeta;
        ColectivoId = colectivo.Id;
        TarjetaId = tarjeta.Id;
        Precio = precio;
        Fecha = fecha ?? DateTime.Now;
        Linea = colectivo.Linea;
        TipoTarjeta = tarjeta.Tipo?.Nombre ?? "Normal";
        TotalAbonado = totalAbonado ?? precio;
        SaldoRestante = saldoRestante ?? tarjeta.Saldo;
    }

    public DateTime GetFecha() => Fecha;
    public string GetTipoTarjeta() => TipoTarjeta;
    public string GetLinea() => Linea;
    public int GetTotalAbonado() => TotalAbonado;
    public int GetSaldo() => SaldoRestante;
    public int GetIdTarjeta() => TarjetaId;
    public int GetSaldoRestante() => SaldoRestante;
}
