using InventarioSimulador.Entidad;
using InventarioSimulador.Modelos;
using Microsoft.EntityFrameworkCore;

namespace InventarioSimulador.data;

    public class SimulacionContext : DbContext
    {
        public SimulacionContext(DbContextOptions<SimulacionContext> options) : base(options) { }

    public DbSet<Simulacion> Simulaciones { get; set; }
         public DbSet<Material> Materiales { get; set; }

    }


