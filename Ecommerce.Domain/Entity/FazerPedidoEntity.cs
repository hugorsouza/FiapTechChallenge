namespace Ecommerce.Domain.Entity
{
    public class FazerPedidoEntity
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public DateTime DataPedido { get; set; }
        public bool Status { get; set; }
        public bool TipoPedido { get; set; }
    }

}
