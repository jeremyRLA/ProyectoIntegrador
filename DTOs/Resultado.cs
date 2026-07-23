using System.Text.Json.Serialization;

namespace UTNGolCoinApi.DTOs
{
    public class Resultado
    {
        [JsonPropertyName("resultadoOficial")]
        public int ResultadoOficial { get; set; } 
    }
}