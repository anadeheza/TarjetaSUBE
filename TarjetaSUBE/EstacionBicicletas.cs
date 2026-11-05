namespace Sube;

public class EstacionBicicletas
{
    public const decimal TarifaDia = 2418.75m;
    public const decimal Multa = 1250m;
    public static readonly TimeSpan TiempoMaximo = TimeSpan.FromHours(1);

    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public EstacionBicicletas() { }

    public EstacionBicicletas(string nombre)
    {
        Nombre = nombre;
    }

    public bool Retirar(Tarjeta tarjeta, DateTime? fecha = null)
    {
        var ahora = fecha ?? DateTime.Now;

        if (tarjeta.TieneBiciFuera)
            return false;

        decimal aPagar = TarifaDia + tarjeta.MultasBiciPendientes;
        if (!tarjeta.DescontarMontoFijo(aPagar))
            return false;

        tarjeta.RegistrarRetiroBici(ahora);
        return true;
    }

    public bool Devolver(Tarjeta tarjeta, DateTime? fecha = null)
    {
        var ahora = fecha ?? DateTime.Now;

        if (!tarjeta.TieneBiciFuera)
            return false;

        tarjeta.RegistrarDevolucionBici(ahora, TiempoMaximo, Multa);
        return true;
    }
}
