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
