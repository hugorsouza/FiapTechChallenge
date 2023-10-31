using Ecommerce.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain.Entities.Pedidos
{
    public class Pedido : Entidade
    {
        public Pedido()
        {
        }

        public Pedido(
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
        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Documento do usuario obrigatório")]
        public string UsuarioDocumento { get; set; }

        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Usuario obrigatório")]
        public string Usuario { get; set; }
        
        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Descrição obrigatório")]
        public string Descricao { get; set; }
       
        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Quantidade obrigatório")]
        public int Quantidade { get; set; }
        
        [Required]
        [DataType(DataType.Currency, ErrorMessage = "ValorUnitario obrigatório")]
        public decimal ValorUnitario { get; set; }

        [Required]
        [DataType(DataType.Currency, ErrorMessage = "ValorTotal obrigatório")]
        public decimal ValorTotal{ get; set; }

        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Data do pedido obrigatório")]
        public DateTime DataPedido { get; set; }
        
        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Tipo de pedido obrigatório")]
        public int TipoPedido { get; set; }

        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Tipo pedido descrição obrigatório")]
        public string TipoPedidoDescricao { get; set; }

        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Status descrição obrigatório")]
        public int Status { get; set; }

        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Status obrigatório")]
        public string StatusDescricao { get; set; }
    }
}
