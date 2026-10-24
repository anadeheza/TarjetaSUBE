using System;

namespace Sube;

public class TarjetaTipo
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Descuento { get; set; }

    public TarjetaTipo() { }

    public TarjetaTipo(int id, string nombre, decimal descuento)
    {
        Id = id;
        Nombre = nombre;
        Descuento = descuento;
    }
}
