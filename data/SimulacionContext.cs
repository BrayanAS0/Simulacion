using InventarioSimulador.Entidad;
using Microsoft.EntityFrameworkCore;

namespace InventarioSimulador.data;

    public class SimulacionContext : DbContext
    {
        public SimulacionContext(DbContextOptions<SimulacionContext> options) : base(options) { }

        public DbSet<Simulacion> Simulaciones { get; set; }
    }


