namespace Ecommerce.Application.Model
{
    public class FazerPedidoModel
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public DateTime DataPedido { get; set; }
        public int Status { get; set; }
        public int TipoPedido { get; set; }
    }
}
