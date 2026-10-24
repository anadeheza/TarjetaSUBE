using Sube;

var tarjeta = new Tarjeta(1);

Console.WriteLine($"Tarjeta {tarjeta.Id}. Saldo: ${tarjeta.Saldo}\n");

var colectivo = new Colectivo("102");

var boleto = colectivo.PagarCon(tarjeta);

if (boleto != null)
      Console.WriteLine($"Pago OK, Linea: {colectivo.Linea}, Precio: ${boleto.Precio} Saldo restante: ${tarjeta.Saldo}");
  else
      Console.WriteLine($"Pago rechazado, Saldo: ${tarjeta.Saldo}");

if (tarjeta.Cargar(2000))
      Console.WriteLine($"Carga OK. Saldo: ${tarjeta.Saldo}");
  else
      Console.WriteLine("Carga rechazada.");

boleto = colectivo.PagarCon(tarjeta);

if (boleto != null)
      Console.WriteLine($"Pago OK, Linea: {colectivo.Linea}, Precio: ${boleto.Precio} Saldo restante: ${tarjeta.Saldo}");
  else
      Console.WriteLine($"Pago rechazado, Saldo: ${tarjeta.Saldo}");
