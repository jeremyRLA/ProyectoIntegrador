namespace UTNGolCoinApi.DTOs
{
    public class RespuestaApiDto
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public decimal NuevoSaldo { get; set; } 
    }
}