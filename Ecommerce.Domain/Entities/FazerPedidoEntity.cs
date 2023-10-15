namespace Ecommerce.Domain.Entities

{
    public class FazerPedidoEntity
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public DateTime DataPedido { get; set; }
        public int Status { get; set; }
        public int TipoPedido { get; set; }
    }
}
