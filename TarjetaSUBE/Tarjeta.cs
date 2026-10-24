using System;

namespace Sube
{
    public class Tarjeta
    {
        public const int TarifaBasica = 1580;
        private const int SaldoMax = 40000;
        private static int[] MontosValidos = { 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000 };

        public int Id { get; set; }
        public int saldo;
        public int Saldo => saldo;

        public Tarjeta() { }

        public Tarjeta(int id)
        {
            this.Id = id;
            this.saldo = 0;
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
            if(saldo < monto) return false;

            saldo -= monto;
            return true;
        }
    }
}