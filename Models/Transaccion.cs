namespace UTNGolCoinApi.Models
{
    public class Transaccion
    {
        public int Id { get; set; }
        public int BilleteraId { get; set; }
        public required Billetera Billetera { get; set; }

        public required string Tipo { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaTransaccion { get; set; } = DateTime.UtcNow;

        public string? Descripcion { get; set; }
    }
}
