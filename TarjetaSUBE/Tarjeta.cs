using System;

namespace Sube
{
    public class Tarjeta
    {
        public int saldo;
        public int id;

        public Tarjeta(int id)
        {
            this.id = id;
            this.saldo = 0;
        }

        public void pagoRealizado()
        {
            saldo -= 1580;
        }

        public void cargaRealizada(int monto)
        {
            if (monto == 2000 || monto == 3000 || monto == 4000 || monto == 5000 ||
                monto == 8000 || monto == 10000 || monto == 15000 || monto == 20000 ||
                monto == 25000 || monto == 30000)
            {
                saldo += monto;
            }
            else
            {
                Console.Error.WriteLine("Solo se aceptan los montos $2000, $3000, $4000, $5000, $8000, $10000, $15000, $20000, $25000 y $30000");
            }
        }
    }
}