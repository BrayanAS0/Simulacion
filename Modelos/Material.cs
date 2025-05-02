namespace InventarioSimulador.Modelos
{
    public class Material
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public int StockActual { get; set; }         // Inventario actual

    }

}
