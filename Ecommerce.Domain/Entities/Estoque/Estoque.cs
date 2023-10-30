using Ecommerce.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain.Entities.Estoque
{
    public class Estoque : Entidade
    {
        public Estoque()
        {
        }

        public Estoque(
            string usuarioDocumento,
            string usuario,
            int produtoId,
            int quantidadeAtual,
            DateTime dataUltimaMovimentacao
            )
        {
            UsuarioDocumento = usuarioDocumento;
            Usuario = usuario;
            ProdutoId = produtoId;
            QuantidadeAtual = quantidadeAtual;
            DataUltimaMovimentacao = dataUltimaMovimentacao;
        }

        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Documento do usuario obrigatório")]
        public string UsuarioDocumento { get; set; }

        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Usuario obrigatório")]
        public string Usuario { get; set; }
        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Produto obrigatório")]
        public int ProdutoId { get; set; }

        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Quantidade obrigatório")]
        public int QuantidadeAtual { get; set; }

        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Data de movimentação obrigatório")]
        public DateTime DataUltimaMovimentacao { get; set; }
    }
}
