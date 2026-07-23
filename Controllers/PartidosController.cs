using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UTNGolCoinApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UTNGolCoinApi.DTOs;

namespace UTNGolCoinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidosController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiErickUrl;

        public PartidosController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiErickUrl = configuration["ServiciosExternos:ApiErickUrl"];
        }

        [HttpGet("disponibles")]
        public async Task<IActionResult> ObtenerPartidosDisponibles()
        {
            try
            {
               
                string rutaEndpointErick = $"{_apiErickUrl}/api-estadisticas/api/partidos";
                var response = await _httpClient.GetAsync(rutaEndpointErick);

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, $"Error al conectar con Java. Código: {response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var todosLosPartidos = JsonSerializer.Deserialize<List<PartidosDto>>(json, opciones);

                if (todosLosPartidos == null || !todosLosPartidos.Any())
                    return Ok(new List<PartidosDto>());

                
                var partidosDisponibles = todosLosPartidos
                    .Where(p => p.estado != null && p.estado.ToUpper() == "PROGRAMADO")
                    .OrderBy(p => p.fecha_hora_utc)
                    .Select(p => {
                        p.cuota_local = 2.1m;
                        p.cuota_empate = 3.0m;
                        p.cuota_visitante = 1.8m;
                        return p;
                    })
                    .ToList();

                return Ok(partidosDisponibles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar los partidos.", detalle = ex.Message });
            }
        }
    }
}