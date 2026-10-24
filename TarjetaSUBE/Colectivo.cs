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

    public Boleto? PagarCon(Tarjeta tarjeta)
    {
        int precio = tarjeta.CalcularBoleto(Tarjeta.TarifaBasica);

        if (!tarjeta.pagoRealizado(Tarjeta.TarifaBasica))
            return null;

        return new Boleto(this, tarjeta, precio);
    }
}
