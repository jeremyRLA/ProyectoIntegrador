namespace UTNGolCoinApi.DTOs
{
    public class Transaccion
    {
        public int Id { get; set; }
        public string PartidoCodigo { get; set; }
        public string Resultado { get; set; }
        public decimal Monto { get; set; }
        public bool Ganada { get; set; }
        public DateTime Fecha { get; set; }
    }
}