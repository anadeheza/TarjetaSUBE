using Sube;

var tipoNormal = new TarjetaTipo(1, "Normal", 1.0m);
var tarjeta = new Tarjeta(1, tipoNormal);

Console.WriteLine($"Tarjeta {tarjeta.Id} creada. Saldo: ${tarjeta.Saldo}");

if (tarjeta.CargaRealizada(2000))
{
    Console.WriteLine($"Carga de $2000 OK. Saldo: ${tarjeta.Saldo}");
}

var colectivo = new Colectivo("102");
bool pudoViajar = colectivo.PagarCon(tarjeta);

if (pudoViajar)
{
    Console.WriteLine($"Viaje en linea {colectivo.Linea}. Saldo restante: ${tarjeta.Saldo}");
}
else
{
    Console.WriteLine("No se pudo viajar: saldo insuficiente.");
}