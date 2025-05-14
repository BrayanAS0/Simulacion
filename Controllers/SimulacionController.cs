using InventarioSimulador.data;
using InventarioSimulador.Entidad;
using InventarioSimulador.Metodos;
using InventarioSimulador.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioSimulador.Controllers;

[ApiController]
[Route("api/simulacion")]
public class SimulacionController : ControllerBase
{
    private readonly SimulacionContext db;
    public SimulacionController(SimulacionContext db)
    {
        this.db = db;
    }

    [HttpPost("ejecutar")]
    public async Task<IActionResult> SimularInventario([FromBody] SimulacionRequest request)
    {
        var material = await db.Materiales.FindAsync(request.IdMaterial);
        if (material == null)
        {
            return NotFound("Material no encontrado");
        }

        var resultados = new List<int>();
        var rand = new Random();

        for (int iter = 0; iter < request.Iteraciones; iter++)
        {
            int inventario = material.StockActual;
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
            MaterialId = material.Id,
            Dias = request.Dias,
            PuntoReorden = request.PuntoReorden,
            CantidadReorden = request.CantidadReorden,
            DiasReabastecimiento = request.DiasReabastecimiento,
            Iteraciones = request.Iteraciones,
            PromedioDesabasto = resultados.Average(),
            Fecha = DateTime.Now
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
            .Include(s => s.Material) 
            .OrderByDescending(s => s.Fecha)
            .ToListAsync();

        return Ok(historial);
    }

    [HttpGet("Material")]
    public async Task<IActionResult> ObtenerMaterial()
    {
        var historial = await db.Materiales
            .ToListAsync();

        return Ok(historial);
    }
    [HttpGet("Material/{id:int}")]
    public async Task<IActionResult> ObtenerMaterialbyId(int id)
    {
        var material = await db.Materiales.FindAsync(id);

        return Ok(material);
    }

        [HttpGet("Material/{stock:int}/{nombre}/{descripcion}")]
    public async Task<IActionResult> IngresarMaterial(int stock,string nombre,string descripcion)
    {
        string patron = "L{3}-L{3}-d{4}";

        var simulacion = new SimulacionMetodos(); // Asegúrate de reemplazar esto con el nombre correcto de la clase que implementa ValidarCadenaConPatron
        var isValid = simulacion.ValidarCadenaConPatron(nombre, patron);
        if (!isValid)
        {
            return Conflict("no ahi coherencia en el patron");
        }
        var material = new Material
        {
            Descripcion = descripcion,
            StockActual = stock,
            Nombre = nombre
        };

        await db.Materiales.AddAsync(material);
        await db.SaveChangesAsync();
        return Ok("exitoso");
            }

    [HttpPost("Material")]
    public async Task<ActionResult> AgregarMateria(AgregarMaterialDto materialDto)
    {

        var isValid = new SimulacionMetodos().ValidarCadenaConPatron(materialDto.Nombre,"");

        var material = new Material {Nombre=materialDto.Nombre,StockActual=materialDto.StockActual,StoreAddress=materialDto.StoreAddress,Descripcion=materialDto.Descripcion };

        await db.Materiales.AddAsync(material);
        await db.SaveChangesAsync();
        return Ok(material);
    }

}
