using InventarioSimulador.data;
using InventarioSimulador.Entidad;
using InventarioSimulador.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioSimulador.Controllers;

[ApiController]
[Route("api/simulacion")]
public class SimulacionController : ControllerBase
{
    public SimulacionContext db;
    public SimulacionController(SimulacionContext db)
    {
        this.db = db;
    }
    [HttpPost("ejecutar")]
    public async Task<IActionResult> SimularInventario(
        [FromBody] SimulacionRequest request)
    {
        var resultados = new List<int>();
        var rand = new Random();

        for (int iter = 0; iter < request.Iteraciones; iter++)
        {
            int inventario = request.StockInicial;
            int desabasto = 0;

            for (int dia = 1; dia <= request.Dias; dia++)
            {
                int demanda = rand.Next(5, 20);

                if (demanda > inventario)
                {
                    desabasto += demanda - inventario;
                    inventario = 0;
                }
                else
                {
                    inventario -= demanda;
                }

                if (dia % request.DiasReabastecimiento == 0 && inventario <= request.PuntoReorden)
                {
                    inventario += request.CantidadReorden;
                }
            }

            resultados.Add(desabasto);
        }

        var simulacion = new Simulacion
        {
            Dias = request.Dias,
            StockInicial = request.StockInicial,
            PuntoReorden = request.PuntoReorden,
            CantidadReorden = request.CantidadReorden,
            DiasReabastecimiento = request.DiasReabastecimiento,
            Iteraciones = request.Iteraciones,
            PromedioDesabasto = resultados.Average()
        };

        db.Simulaciones.Add(simulacion);
        await db.SaveChangesAsync();

        return Ok(new
        {
            PromedioDesabasto = simulacion.PromedioDesabasto,
            MinimoDesabasto = resultados.Min(),
            MaximoDesabasto = resultados.Max()
        });
    }

    [HttpGet("historial")]
    public async Task<IActionResult> ObtenerHistorial()
    {
        var historial = await db.Simulaciones
            .OrderByDescending(s => s.Fecha)
            .ToListAsync();

        return Ok(historial);
    }
}
