namespace InventarioSimulador.Modelos
{
    public class AgregarMaterialDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public int StockActual { get; set; }         // Inventario actual
        public string? StoreAddress { get; set; }
    }
}
