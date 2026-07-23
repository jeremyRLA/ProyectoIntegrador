using Microsoft.AspNetCore.Mvc;
using UTNGolCoinApi.Data;
using UTNGolCoinApi.Dtos;
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
            public string PartidoCodigo { get; set; }
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
                BilleteraId = billetera.Id,
                UsuarioId = request.UsuarioId, 
                PartidoId = request.PartidoCodigo, 
                PartidoCodigo = request.PartidoCodigo,
                ResultadoPronosticado = request.ResultadoPronosticado,
                MontoApostado = request.MontoApostado,
                Cuota = request.Cuota,
                Estado = "PENDIENTE",
                FechaRegistro = DateTime.UtcNow
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

            bool yaPredijo = _context.Predicciones.Any(p =>
    p.UsuarioId == request.UsuarioId &&
    p.PartidoCodigo == request.PartidoCodigo);

            if (yaPredijo)
            {
                return BadRequest(new { mensaje = "Ya realizaste una predicción para este partido." });
            }

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

        [HttpPost("liquidar/{codigoPartido}")]
        public IActionResult LiquidarPredicciones(string codigoPartido, [FromBody] Resultado request)
        {
            var prediccionesPendientes = _context.Predicciones
                .Where(p => p.PartidoCodigo == codigoPartido && p.Estado == "PENDIENTE")
                .ToList();

            if (!prediccionesPendientes.Any())
            {
                return Ok(new { mensaje = "No hay predicciones pendientes para este partido." });
            }

            int ganadores = 0;
            int perdedores = 0;
            decimal totalPagado = 0;

            foreach (var prediccion in prediccionesPendientes)
            {
                if (prediccion.ResultadoPronosticado == request.ResultadoOficial)
                {
                    prediccion.Estado = "GANADA";
                    ganadores++;

                    decimal premio = prediccion.MontoApostado * prediccion.Cuota;

                    var billetera = _context.Billeteras.Find(prediccion.BilleteraId);
                    if (billetera != null)
                    {
                        billetera.Saldo += premio;
                        totalPagado += premio;

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
                }
                else
                {
                    prediccion.Estado = "PERDIDA";
                    perdedores++;
                }
            }

            _context.SaveChanges();

            return Ok(new
            {
                exito = true,
                mensaje = $"Liquidación completada para el partido {codigoPartido}.",
                estadisticas = new
                {
                    ganadores,
                    perdedores,
                    monedasRepartidas = totalPagado
                }
            });
        }
    }
}