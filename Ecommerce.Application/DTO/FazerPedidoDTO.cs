namespace Ecommerce.Application.DTO
{
    public class FazerPedidoDTO
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public DateTime DataPedido { get; set; }
        public bool Status { get; set; }
        public bool TipoPedido { get; set; }
    }
}
