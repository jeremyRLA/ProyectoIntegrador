namespace UTNGolCoinApi.Models
{
    public class Prediccion
    {
        public int Id { get; set; }
        public required string UsuarioId { get; set; }
        public required string PartidoId { get; set; }

        public required string ResultadoPronosticado { get; set; }
        public decimal MontoApostado { get; set; }
        public decimal Cuota { get; set; }

        public string Estado { get; set; } = "PENDIENTE";
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}