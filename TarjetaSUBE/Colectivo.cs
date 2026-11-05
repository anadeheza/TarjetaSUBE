namespace Sube
{
    public class Colectivo
    {
        public int Id { get; set; }
        public string Linea { get; set; } = string.Empty;
        public int Tarifa { get; set; }

        public Colectivo() 
        {
            Tarifa = Tarjeta.TarifaBasica;
        }

        public Colectivo(string linea, int tarifa = Tarjeta.TarifaBasica)
        {
            Linea = linea;
            Tarifa = tarifa;
        }

        public Boleto? PagarCon(Tarjeta tarjeta, DateTime? fecha = null)
        {
            var ahora = fecha ?? DateTime.Now;

            if (!tarjeta.PuedeViajar(ahora))
                return null;

            tarjeta.ActualizarDia(ahora);

            bool esTrasbordo = tarjeta.EsTrasbordo(Linea, ahora);
            int saldoAnterior = tarjeta.Saldo;
            int precio = tarjeta.CalcularBoleto(Tarifa, Linea, ahora);

            if (!tarjeta.pagoRealizado(Tarifa, Linea, ahora))
                return null;

            int totalAbonado = precio;
            if (saldoAnterior < 0 && precio > 0)
                totalAbonado = precio - saldoAnterior;

            return new Boleto(colectivo: this, tarjeta: tarjeta, precio: precio, totalAbonado: totalAbonado, saldoRestante: tarjeta.Saldo, fecha: ahora, esTrasbordo: esTrasbordo);
        }
    }
}