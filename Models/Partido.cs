using System;

namespace UTNGolCoinApi.Models
{
    public class Partido
    {
        public int Id { get; set; }
        public required string Codigo { get; set; }
        public string Estado { get; set; } = "PROGRAMADO";
        public DateTime FechaPartido { get; set; }
        public string? Fase { get; set; } 
        public string? Grupo { get; set; }

        public int? NumeroPartidoFifa { get; set; }

        public int? SedeId { get; set; }
        public string? NombreSede { get; set; }

        public string? SeleccionLocal { get; set; } 
        public int? SeleccionLocalId { get; set; }

        public string? SeleccionVisitante { get; set; }
        public int? SeleccionVisitanteId { get; set; }
        public int? GolesLocal { get; set; }
        public int? GolesVisitante { get; set; }

        public decimal CuotaLocal { get; set; }
        public decimal CuotaEmpate { get; set; }
        public decimal CuotaVisitante { get; set; }
    }
}