using Microsoft.AspNetCore.Mvc;
using UTNGolCoinApi.Data;
using UTNGolCoinApi.Models;

namespace UTNGolCoinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BilleterasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BilleterasController(AppDbContext context)
        {
            _context = context;
        }

        public class CrearBilleteraRequest
        {
            public string UsuarioId { get; set; }
        }

        [HttpPost("crear")]
        public IActionResult CrearBilletera([FromBody] CrearBilleteraRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UsuarioId))
            {
                return BadRequest(new { mensaje = "El ID del usuario es obligatorio." });
            }

            var existe = _context.Billeteras.Any(b => b.UsuarioId == request.UsuarioId);
            if (existe)
            {
                return BadRequest(new { mensaje = "Este usuario ya tiene una billetera registrada." });
            }

            var nuevaBilletera = new Billetera
            {
                UsuarioId = request.UsuarioId,
                Saldo = 10.00m
            };

            _context.Billeteras.Add(nuevaBilletera);
            _context.SaveChanges();

            var transaccion = new Transaccion
            {
                BilleteraId = nuevaBilletera.Id,
                Billetera = nuevaBilletera,
                Tipo = "BonoBienvenida",
                Monto = 10.00m,
                FechaTransaccion = DateTime.UtcNow
            };

            _context.Transacciones.Add(transaccion);
            _context.SaveChanges(); 

            return Ok(new
            {
                exito = true,
                mensaje = "Billetera creada exitosamente con el bono de bienvenida.",
                saldoActual = nuevaBilletera.Saldo
            });
        }

        [HttpGet("saldo/{usuarioId}")]
        public IActionResult ConsultarSaldo(string usuarioId)
        {
            var billetera = _context.Billeteras.FirstOrDefault(b => b.UsuarioId == usuarioId);

            if (billetera == null)
            {
                return NotFound(new { mensaje = "No se encontró una billetera para este usuario." });
            }

            return Ok(new
            {
                usuarioId = billetera.UsuarioId,
                saldo = billetera.Saldo
            });
        }
        [HttpGet("{usuarioId}/transacciones")]
        public IActionResult ObtenerHistorialTransacciones(string usuarioId)
        {
            var billetera = _context.Billeteras.FirstOrDefault(b => b.UsuarioId == usuarioId);

            if (billetera == null)
            {
                return NotFound(new { mensaje = "No se encontró la billetera del usuario." });
            }

            var transacciones = _context.Transacciones
                .Where(t => t.BilleteraId == billetera.Id)
                .OrderByDescending(t => t.FechaTransaccion)
                .Select(t => new
                {
                    t.Id,
                    t.Tipo,
                    t.Monto,
                    t.FechaTransaccion
                })
                .ToList();

            return Ok(new
            {
                usuarioId = billetera.UsuarioId,
                saldoActual = billetera.Saldo,
                historial = transacciones
            });
        }
        public class ResolverPrediccionRequest
        {
            public string ResultadoReal { get; set; }
        }

        [HttpPut("{id}/resolver")]
        public IActionResult ResolverPrediccion(int id, [FromBody] ResolverPrediccionRequest request)
        {
            var prediccion = _context.Predicciones.FirstOrDefault(p => p.Id == id);

            if (prediccion == null)
            {
                return NotFound(new { mensaje = "Predicción no encontrada." });
            }

            if (prediccion.Estado != "PENDIENTE")
            {
                return BadRequest(new { mensaje = "Esta predicción ya fue resuelta anteriormente." });
            }

            var billetera = _context.Billeteras.FirstOrDefault(b => b.UsuarioId == prediccion.UsuarioId);

            if (billetera == null)
            {
                return NotFound(new { mensaje = "No se encontró la billetera asociada a esta predicción." });
            }

            bool gano = prediccion.ResultadoPronosticado.ToUpper() == request.ResultadoReal.ToUpper();

            if (gano)
            {
                prediccion.Estado = "GANADA";

                decimal premio = prediccion.MontoApostado * prediccion.Cuota;
                billetera.Saldo += premio;

                var transaccion = new Transaccion
                {
                    BilleteraId = billetera.Id,
                    Billetera = billetera,
                    Tipo = "Premio",
                    Monto = premio,
                    FechaTransaccion = DateTime.UtcNow
                };

                _context.Transacciones.Add(transaccion);
            }
            else
            {
                prediccion.Estado = "PERDIDA";
            }

            _context.SaveChanges();

            return Ok(new
            {
                exito = true,
                mensaje = gano ? "¡Predicción ganada! Premio acreditado exitosamente." : "Predicción perdida. Suerte para la próxima.",
                estadoNuevo = prediccion.Estado,
                saldoActual = billetera.Saldo
            });
        }
    }
}