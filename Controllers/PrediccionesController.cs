using Microsoft.AspNetCore.Mvc;
using UTNGolCoinApi.Data;
using UTNGolCoinApi.Models;

namespace UTNGolCoinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrediccionesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrediccionesController(AppDbContext context)
        {
            _context = context;
        }

        public class CrearPrediccionRequest
        {
            public string UsuarioId { get; set; }
            public string PartidoId { get; set; }
            public string ResultadoPronosticado { get; set; }
            public decimal MontoApostado { get; set; }
            public decimal Cuota { get; set; }
        }

        [HttpPost("crear")]
        public IActionResult CrearPrediccion([FromBody] CrearPrediccionRequest request)
        {
            if (request.MontoApostado <= 0)
            {
                return BadRequest(new { mensaje = "El monto apostado debe ser mayor a cero." });
            }

            var billetera = _context.Billeteras.FirstOrDefault(b => b.UsuarioId == request.UsuarioId);

            if (billetera == null)
            {
                return NotFound(new { mensaje = "No se encontró la billetera del usuario." });
            }

            if (billetera.Saldo < request.MontoApostado)
            {
                return BadRequest(new { mensaje = "Saldo insuficiente para realizar esta predicción." });
            }

            billetera.Saldo -= request.MontoApostado;
            var nuevaPrediccion = new Prediccion
            {
                UsuarioId = request.UsuarioId,
                PartidoId = request.PartidoId,
                ResultadoPronosticado = request.ResultadoPronosticado,
                MontoApostado = request.MontoApostado,
                Cuota = request.Cuota
            };

            _context.Predicciones.Add(nuevaPrediccion);

            var transaccion = new Transaccion
            {
                BilleteraId = billetera.Id,
                Billetera = billetera,
                Tipo = "Prediccion",
                Monto = -request.MontoApostado, 
                FechaTransaccion = DateTime.UtcNow
            };

            _context.Transacciones.Add(transaccion);

            _context.SaveChanges();

            return Ok(new
            {
                exito = true,
                mensaje = "Predicción registrada exitosamente.",
                saldoRestante = billetera.Saldo,
                prediccionId = nuevaPrediccion.Id
            });
        }
        [HttpGet("historial/{usuarioId}")]
        public IActionResult ObtenerPrediccionesUsuario(string usuarioId)
        {
            var billetera = _context.Billeteras.FirstOrDefault(b => b.UsuarioId == usuarioId);

            if (billetera == null)
            {
                return NotFound(new { mensaje = "No se encontró la billetera del usuario." });
            }

            var predicciones = _context.Predicciones
                .Where(p => p.BilleteraId == billetera.Id)
                .OrderByDescending(p => p.FechaRegistro)
                .ToList();

            return Ok(predicciones);
        }
    }
}