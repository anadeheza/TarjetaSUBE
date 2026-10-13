using System;

namespace Sube;

public class Tarjeta
{
    public int Id { get; set; }
    public int Saldo { get; set; }

    public int Id_Tipo { get; set; }
    public TarjetaTipo Tipo { get; set; } = null!;

    public Tarjeta() { }

    public Tarjeta(int id, TarjetaTipo tipo)
    {
        Id = id;
        Saldo = 0;
        Tipo = tipo;
        Id_Tipo = tipo.Id;
    }

    public int CalcularBoleto(int tarifaBase)
    {
        if (Tipo is null)
            return tarifaBase;

        return (int)Math.Round(tarifaBase * Tipo.Descuento);
    }

    public bool PagoRealizado(int tarifa)
    {
        int costo = CalcularBoleto(tarifa);

        if (costo == 0)
            return true;

        if (Saldo < costo)
            return false;

        Saldo -= costo;
        return true;
    }

    public bool CargaRealizada(int monto)
    {
        if (monto == 2000 || monto == 3000 || monto == 4000 || monto == 5000 ||
            monto == 8000 || monto == 10000 || monto == 15000 || monto == 20000 ||
            monto == 25000 || monto == 30000)
        {
            Saldo += monto;
            return true;
        }

        Console.Error.WriteLine("Solo se aceptan los montos $2000, $3000, $4000, $5000, $8000, $10000, $15000, $20000, $25000 y $30000");
        return false;
    }
}