using Microsoft.AspNetCore.Mvc;
using UTNGolCoinApi.Data;
using UTNGolCoinApi.Models;


namespace UTNGolCoinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PartidosController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("crear")]
        public IActionResult CrearPartido([FromBody] Partido partido)
        {
            _context.Partidos.Add(partido);
            _context.SaveChanges();

            return Ok( new
                { exito = true,
                  mensaje = "Partido creado exitosamente",      
                  partido = partido
            });
        }
        [HttpGet("disponibles")]
        public IActionResult ObtenerPartidosDisponibles()
        {
            var partidos = _context.Partidos
            .Where(p => p.Estado=="PROGRAMADO")
            .OrderBy(p => p.FechaPartido)
            .ToList();

            return Ok(partidos);
        }
    }
}
