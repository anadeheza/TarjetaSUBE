namespace Sube;

public class Colectivo
{
    public int Id { get; set; }
    public string Linea { get; set; } = "";

    public Colectivo() { }

    public Colectivo(string linea)
    {
        Linea = linea;
    }

    public Boleto? PagarCon(Tarjeta tarjeta)
    {
        if (!tarjeta.Descontar(Tarjeta.TarifaBasica))
        {
            return null;
        }

        return new Boleto(this, tarjeta, Tarjeta.TarifaBasica);
    }
}
