namespace UTNGolCoinApi.Models
{
    public class BonoDiario
    {
        public int Id { get; set; }
        public int BilleteraId { get; set; }
        public DateTime FechaBono { get; set; }
        public decimal Monto { get; set; }
        public Billetera? Billetera { get; set; }
    }
}
