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

    public bool PagarCon(Tarjeta tarjeta)
    {
        int tarifa = 1580;
        return tarjeta.PagoRealizado(tarifa);
    }
}