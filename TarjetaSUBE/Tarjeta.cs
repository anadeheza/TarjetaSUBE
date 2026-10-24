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

        public int CalcularBoleto(int tarifaBase)
        {
            if (Tipo is null)
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


        public bool pagoRealizado(int monto)
        {
            int costo = CalcularBoleto(monto);

            if (costo == 0)
                return true;

            if ((saldo - costo) < SaldoMin) return false;

            saldo -= costo;
            return true;
        }
    }
}