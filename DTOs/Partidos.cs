namespace UTNGolCoinApi.DTOs
{
    using System;

    public class Partidos
    {
        public int? id { get; set; }
        public int? numero_partido_fifa { get; set; }
        public string fase { get; set; }
        public string? fecha_hora_utc { get; set; }
        public string? fecha_hora_et { get; set; }
        public string estado { get; set; }
        public string nombre_sede { get; set; }
        public int? sede_id { get; set; }
        public int? seleccion_local_id { get; set; }
        public int? seleccion_visitante_id { get; set; }
        public string seleccion_local { get; set; }
        public string seleccion_visitante { get; set; }
        public int? goles_local { get; set; }
        public int? goles_visitante { get; set; }
        public string grupo { get; set; }
        public decimal cuota_local { get; set; }
        public decimal cuota_empate { get; set; }
        public decimal cuota_visitante { get; set; }
    }
}
