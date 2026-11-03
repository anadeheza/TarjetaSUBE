using System;

namespace Sube
{
    public class Tarjeta
    {
        public const int TarifaBasica = 1580;
        private const int SaldoMax = 40000;
        private const int SaldoMin = -2000;
        private static int[] MontosValidos = { 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000 };

        public int Id { get; set; }
        public int saldo;
        public int Saldo => saldo;

        public int Id_Tipo { get; set; }
        public TarjetaTipo Tipo { get; set; } = null!;

        public DateTime? UltimoViaje { get; private set; }
        public bool EsMedioBoleto => Tipo?.Nombre == "Medio boleto estudiantil";
        public int ViajesHoy { get; private set; }

        public Tarjeta() { }

        public Tarjeta(int id)
        {
            this.Id = id;
            this.saldo = 0;
        }

        public Tarjeta(int id, TarjetaTipo tipo)
        {
            Id = id;
            saldo = 0;
            Tipo = tipo;
            Id_Tipo = tipo.Id;
        }

        public void ActualizarDia(DateTime ahora)
        {
            if (UltimoViaje is null || UltimoViaje.Value.Date != ahora.Date)
                ViajesHoy = 0;
        }

        public int CalcularBoleto(int tarifaBase)
        {
            if (Tipo is null)
                return tarifaBase;
            if (EsMedioBoleto && ViajesHoy >= 2)
                return tarifaBase;

            return (int)Math.Round(tarifaBase * Tipo.Descuento);
        }

        public bool Cargar(int monto)
        {
            if(!MontosValidos.Contains(monto)) return false; 
            if(saldo + monto > SaldoMax) return false;

            saldo += monto;

            return true;
        }


        public bool pagoRealizado(int monto, DateTime? fecha = null)
        {
            var ahora = fecha ?? DateTime.Now;
            ActualizarDia(ahora);

            int costo = CalcularBoleto(monto);

            if (costo == 0)
                return true;

            if ((saldo - costo) < SaldoMin) return false;

            saldo -= costo;
            if (EsMedioBoleto)
                ViajesHoy++;
            UltimoViaje = ahora;
            return true;
        }

        public bool PuedeViajar(DateTime ahora)
        {
            if (!EsMedioBoleto) return true;
            if (UltimoViaje is null) return true;

            return ahora - UltimoViaje.Value >= TimeSpan.FromMinutes(5);
        }
    }
}