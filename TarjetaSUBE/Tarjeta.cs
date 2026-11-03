using System;
using System.Linq;

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

        public int ViajesDelMes { get; private set; } = 0;
        private int ultimoMesViaje = -1;
        private int ultimoAñoViaje = -1;

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

        private void ActualizarContadorDeViajes()
        {
            DateTime hoy = DateTime.Now;

            if (hoy.Month != ultimoMesViaje || hoy.Year != ultimoAñoViaje)
            {
                ViajesDelMes = 0;
                ultimoMesViaje = hoy.Month;
                ultimoAñoViaje = hoy.Year;
            }
        }

        public int CalcularBoleto(int tarifaBase)
        {
            ActualizarContadorDeViajes();

            if (Tipo is null)
                return tarifaBase;

            double costo = tarifaBase * Tipo.Descuento;

            if (Tipo.Descuento == 1.0f)
            {
                int viajesSiguientes = ViajesDelMes + 1;

                if (viajesSiguientes >= 80)
                {
                    costo *= 0.75;
                }
                else if (viajesSiguientes >= 30)
                {
                    costo *= 0.80;
                }
            }

            return (int)Math.Round(costo);
        }

        public bool Cargar(int monto)
        {
            if (!MontosValidos.Contains(monto)) return false; 
            if (saldo + monto > SaldoMax) return false;

            saldo += monto;

            return true;
        }

        public bool pagoRealizado(int monto)
        {
            int costo = CalcularBoleto(monto);

            if (costo == 0)
            {
                ActualizarContadorDeViajes();
                ViajesDelMes++;
                return true;
            }

            if ((saldo - costo) < SaldoMin) return false;

            saldo -= costo;
            ViajesDelMes++;
            return true;
        }
    }
}