namespace UTNGolCoinApi.DTOs
{
    public class Selecciones
    {
        public int? id { get; set; }
        public string codigo_fifa { get; set; }
        public string nombre { get; set; }
        public char? grupo { get; set; } // El 'Character' de Java equivale a un char nullable
        public string confederacion { get; set; }
        public string clasificacion { get; set; }
    }
}
