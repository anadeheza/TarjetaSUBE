using Sube;

var tarjeta = new Tarjeta(1);
Console.WriteLine($"Tarjeta {tarjeta.Id} creada. Saldo: ${tarjeta.Saldo}");

if (tarjeta.CargaRealizada(2000))
{
    Console.WriteLine($"Carga de $2000 OK. Saldo: ${tarjeta.Saldo}");
}

var colectivo = new Colectivo("102");
bool boleto = colectivo.PagarCon(tarjeta);

if (boleto)
{
    Console.WriteLine($"Viaje en linea {colectivo.Linea}. Boleto: $1580. Saldo restante: ${tarjeta.Saldo}");
}
else
{
    Console.WriteLine("No se pudo viajar: saldo insuficiente.");
}