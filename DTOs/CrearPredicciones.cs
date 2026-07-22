namespace UTNGolCoinApi.DTOs
{
    public class CrearPrediccion
    {
        public string UsuarioId { get; set; }
        public string PartidoCodigo { get; set; }
        public string ResultadoPronosticado { get; set; }
        public decimal MontoApostado { get; set; }
        public decimal Cuota { get; set; }
    }
}