

namespace Ecommerce.Application.Model.Pessoas.Estoque
{
    public class EstoqueModel
    {
        public EstoqueModel()
        {
        }

        public EstoqueModel(
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

        public string UsuarioDocumento { get; set; }
        public string Usuario { get; set; }
        public int ProdutoId { get; set; }
        public int QuantidadeAtual { get; set;}
        public DateTime DataUltimaMovimentacao { get; set; }
    }
}
