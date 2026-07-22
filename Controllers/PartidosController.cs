using Microsoft.AspNetCore.Mvc;
using UTNGolCoinApi.Data; 
using UTNGolCoinApi.Models;
using UTNGolCoinApi.DTOs;

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
                .Where(p => p.Estado == "PROGRAMADO")
                .OrderBy(p => p.FechaPartido)
                .Select(p => new PartidosDto
                {
                    id = p.Id,
                    numero_partido_fifa = p.NumeroPartidoFifa,
                    fase = p.Fase,
                    fecha_hora_utc = p.FechaPartido,
                    estado = p.Estado,
                    nombre_sede = p.NombreSede,
                    sede_id = p.SedeId,
                    seleccion_local_id = p.SeleccionLocalId,
                    seleccion_visitante_id = p.SeleccionVisitanteId,
                    seleccion_local = p.SeleccionLocal,
                    seleccion_visitante = p.SeleccionVisitante,
                    goles_local = p.GolesLocal,
                    goles_visitante = p.GolesVisitante,
                    grupo = p.Grupo
                })
                .ToList();

            return Ok(partidos);
        }
    }
}