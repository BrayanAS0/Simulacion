namespace InventarioSimulador.Modelos
{
    public class Simulacion
    {
        public int Id { get; set; }

        public int MaterialId { get; set; }             
        public Material Material { get; set; }          

        public int Dias { get; set; }
        public int PuntoReorden { get; set; }
        public int CantidadReorden { get; set; }
        public int DiasReabastecimiento { get; set; }
        public int Iteraciones { get; set; }

        public double PromedioDesabasto { get; set; }    // Resultado
        public DateTime Fecha { get; set; } = DateTime.Now;
    }

}
