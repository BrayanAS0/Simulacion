namespace InventarioSimulador.Modelos;
    public class SimulacionRequest
    {
        public int Dias { get; set; }//cauantos dias durar la simualcion 
        public int StockInicial { get; set; } //inventario inicial en cada simulacion
        public int PuntoReorden { get; set; } //Si el inventario es 20 o menos, se puede pedir más stock.
    public int CantidadReorden { get; set; } //Cada vez que se reordena, se agregan 50 unidades al inventario.
    public int DiasReabastecimiento { get; set; } //Solo se revisa si hay que reordenar cada 7 días.

    public int Iteraciones { get; set; } //iteracione sque va dar
    }

