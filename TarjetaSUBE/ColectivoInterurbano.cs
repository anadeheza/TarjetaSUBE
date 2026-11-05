namespace Sube
{
    public class ColectivoInterurbano : Colectivo
    {
        public const int TarifaInterurbana = 3000;

        public ColectivoInterurbano() : base()
        {
            Tarifa = TarifaInterurbana;
        }

        public ColectivoInterurbano(string linea) : base(linea, TarifaInterurbana)
        {
        }
    }
}