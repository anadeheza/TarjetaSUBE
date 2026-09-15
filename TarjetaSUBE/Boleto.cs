namespace Sube;

public class Boleto
{
    public int Id { get; set; }
    public int Precio { get; set; }
    public DateTime Fecha { get; set; }

    public int TarjetaId { get; set; }
    public Tarjeta Tarjeta { get; set; } = null!;

    public int ColectivoId { get; set; }
    public Colectivo Colectivo { get; set; } = null!;

    public Boleto() { }

    public Boleto(Colectivo colectivo, Tarjeta tarjeta, int precio)
    {
        Colectivo = colectivo;
        Tarjeta = tarjeta;
        ColectivoId = colectivo.Id;
        TarjetaId = tarjeta.Id;
        Precio = precio;
        Fecha = DateTime.Now;
    }
}
