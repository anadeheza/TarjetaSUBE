using System;
using System.Linq;

namespace Sube
{
    public class Tarjeta
    {
        public const int TarifaBasica = 2150;
        public const int SaldoMax = 56000;
        private const int SaldoMin = -2000;
        private static int[] MontosValidos = { 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000 };

        public int Id { get; set; }
        private decimal saldo;
        public decimal Saldo => saldo;

        public int Id_Tipo { get; set; }
        public TarjetaTipo Tipo { get; set; } = null!;

        public DateTime? UltimoViaje { get; private set; }
        public string? UltimaLineaViajada { get; private set; }

        public bool EsNormal => Tipo is null || Tipo.Nombre == "Normal";
        public bool EsMedioBoleto => Tipo?.Nombre == "Medio boleto estudiantil";
        public bool EsBoletoGratuito => Tipo?.Nombre == "Boleto gratuito estudiantil";

        public DateTime? RetiroBici { get; private set; }
        public decimal MultasBiciPendientes { get; private set; }
        public bool TieneBiciFuera => RetiroBici is not null;

        public int ViajesHoy { get; private set; }
        public decimal SaldoPendiente { get; private set; }

        public int ViajesDelMes { get; private set; }
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

        public void ActualizarDia(DateTime ahora)
        {
            if (UltimoViaje is null || UltimoViaje.Value.Date != ahora.Date)
                ViajesHoy = 0;
        }

        private void ActualizarContadorDeViajes(DateTime ahora)
        {
            if (ahora.Month != ultimoMesViaje || ahora.Year != ultimoAñoViaje)
            {
                ViajesDelMes = 0;
                ultimoMesViaje = ahora.Month;
                ultimoAñoViaje = ahora.Year;
            }
        }

        public bool EsTrasbordo(string lineaActual, DateTime ahora)
        {
            if (UltimoViaje is null || string.IsNullOrEmpty(UltimaLineaViajada))
                return false;

            if (UltimaLineaViajada == lineaActual)
                return false;

            if ((ahora - UltimoViaje.Value).TotalMinutes > 60)
                return false;

            // Lunes a sábado de 7:00 a 22:00
            bool esDiaValido = ahora.DayOfWeek != DayOfWeek.Sunday;
            bool esHorarioValido = ahora.Hour >= 7 && ahora.Hour < 22;

            return esDiaValido && esHorarioValido;
        }

        public int CalcularBoleto(int tarifaBase, string? lineaActual = null, DateTime? fecha = null)
        {
            var ahora = fecha ?? DateTime.Now;
            ActualizarContadorDeViajes(ahora);

            if (lineaActual != null && EsTrasbordo(lineaActual, ahora))
                return 0;

            if ((EsMedioBoleto || EsBoletoGratuito) && ViajesHoy >= 2)
                return tarifaBase;

            if (EsBoletoGratuito)
                return 0;

            if (EsNormal)
            {
                double costo = tarifaBase;
                int siguiente = ViajesDelMes + 1;

                if (siguiente >= 80)
                    costo *= 0.75;
                else if (siguiente >= 30)
                    costo *= 0.80;

                return (int)Math.Round(costo);
            }

            if (Tipo is null)
                return tarifaBase;

            return (int)Math.Round(tarifaBase * Tipo.Descuento);
        }

        public bool Cargar(int monto)
        {
            if (!MontosValidos.Contains(monto)) return false;

            SaldoPendiente += monto;
            AcreditarCarga();
            return true;
        }

        public bool pagoRealizado(int monto, string lineaActual, DateTime? fecha = null)
        {
            var ahora = fecha ?? DateTime.Now;
            ActualizarDia(ahora);

            int costo = CalcularBoleto(monto, lineaActual, ahora);

            if (costo == 0)
            {
                Ultimo