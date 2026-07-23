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
            public string usuario_id { get; set; }
        }

        [HttpGet]
        public IActionResult ObtenerTodasLasBilleteras()
        {
            var billeteras = _context.Billeteras.ToList();
            return Ok(billeteras);
        }

        [HttpPost("crear")]
        public IActionResult CrearBilletera([FromBody] CrearBilleteraRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.usuario_id))
            {
                return BadRequest(new { mensaje = "El ID del usuario es obligatorio." });
            }

            var existe = _context.Billeteras.Any(b => b.UsuarioId == request.usuario_id);
            if (existe)
            {
                return BadRequest(new { mensaje = "Este usuario ya tiene una billetera registrada." });
            }

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var nuevaBilletera = new Billetera
                {
                    UsuarioId = request.usuario_id,
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
                transaction.Commit();

                return Ok(new
                {
                    exito = true,
                    mensaje = "Billetera creada exitosamente con el bono de bienvenida.",
                    saldoActual = nuevaBilletera.Saldo
                });
            }
            catch (Exception)
            {
                transaction.Rollback();
                return StatusCode(500, new { mensaje = "Error interno al crear la billetera." });
            }
        }

        [HttpGet("saldo/{usuarioId}")]
        public IActionResult ConsultarSaldo(string usuarioId)
        {
            var billetera = _context.Billeteras.FirstOrDefault(b => b.UsuarioId == usuarioId);
            if (billetera == null) return NotFound(new { mensaje = "No se encontró una billetera." });

            return Ok(new { usuarioId = billetera.UsuarioId, saldo = billetera.Saldo });
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
                .Select(t => new { t.Id, t.Tipo, t.Monto, t.FechaTransaccion })
                .ToList();

            return Ok(new
            {
                usuarioId = billetera.UsuarioId,
                saldoActual = billetera.Saldo,
                historial = transacciones
            });
        }

        [HttpPost("{usuarioId}/bono")]
        public IActionResult ReclamarBonoDiario(string usuarioId)
        {
            var billetera = _context.Billeteras.FirstOrDefault(b => b.UsuarioId == usuarioId);

            if (billetera == null)
                return NotFound(new { mensaje = "No se encontró la billetera del usuario." });

            if (billetera.Saldo > 0)
                return BadRequest(new { mensaje = "El bono solo aplica cuando el saldo es cero" });

            var hoy = DateTime.UtcNow.Date;
            bool yaReclamo = _context.BonosDiarios.Any(b => b.BilleteraId == billetera.Id && b.FechaBono.Date == hoy);

            if (yaReclamo)
                return BadRequest(new { mensaje = "Ya reclamaste tu bono de hoy. Vuelve mañana." });

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                decimal montoBono = 1.00m;
                billetera.Saldo += montoBono;

                var bono = new BonoDiario
                {
                    BilleteraId = billetera.Id,
                    FechaBono = DateTime.UtcNow,
                    Monto = montoBono
                };

                var transaccion = new Transaccion
                {
                    BilleteraId = billetera.Id,
                    Billetera = billetera,
                    Tipo = "Bono Diario",
                    Monto = montoBono,
                    FechaTransaccion = DateTime.UtcNow
                };

                _context.BonosDiarios.Add(bono);
                _context.Transacciones.Add(transaccion);

                _context.SaveChanges();
                transaction.Commit();

                return Ok(new
                {
                    exito = true,
                    mensaje = $"¡Bono diario de {montoBono} monedas reclamado exitosamente!",
                    saldoActual = billetera.Saldo
                });
            }
            catch (Exception)
            {
                transaction.Rollback();
                return StatusCode(500, new { mensaje = "Ocurrió un error al procesar el bono diario." });
            }
        }
    }
}