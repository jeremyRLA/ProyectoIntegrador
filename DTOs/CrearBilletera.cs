using System.Text.Json.Serialization;

namespace UTNGolCoinApi.DTOs
{
    public class CrearBilletera
    {
        [JsonPropertyName("usuario_id")] 
        public string UsuarioId { get; set; }
    }
}
