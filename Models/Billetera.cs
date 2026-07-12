namespace UTNGolCoinApi.Models
{
    public class Billetera
    {
        public int Id { get; set; }
        public required string UsuarioId { get; set; }
        public decimal Saldo { get; set; }

        public List<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
    }
}