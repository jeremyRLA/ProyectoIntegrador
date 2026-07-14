namespace UTNGolCoinApi.Models
{
    public class Partido
    {
        public int Id { get; set; }
        public required string Codigo { get; set; }
        public required string Equipo1 { get; set; }
        public required string Equipo2 { get; set; }
        public DateTime FechaPartido { get; set; }
        public decimal CuotaLocal { get; set; }
        public decimal CuotaEmpate { get; set; }
        public decimal CuotaVisitante { get; set; }
        public string Estado { get; set; } = "PROGRAMADO";
    }
}