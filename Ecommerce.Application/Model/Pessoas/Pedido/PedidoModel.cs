using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.Model.Pessoas.Pedido
{
    public class PedidoModel
    {
        public PedidoModel()
        {
            
        }
        public PedidoModel(
           string usuarioDocumento,
           string usuario,
           string descricao,
           int quantidade,
           decimal valorUnitario,
           decimal valorTotal,
           DateTime dataPedido,
           int tipoPedido,
           string tipoPedidoDescricao,
           int status,
           string statusDescricao
           )
        {
            UsuarioDocumento = usuarioDocumento;
            Usuario = usuario;
            Descricao = descricao;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            ValorTotal = valorTotal;
            DataPedido = dataPedido;
            TipoPedido = tipoPedido;
            TipoPedidoDescricao = tipoPedidoDescricao;
            Status = status;
            StatusDescricao = statusDescricao;
        }

        public string UsuarioDocumento { get; set; }
        public string Usuario { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataPedido { get; set; }
        public int TipoPedido { get; set; }
        public string TipoPedidoDescricao { get; set; }
        public int Status { get; set; }
        public string StatusDescricao { get; set; }
    }
}
