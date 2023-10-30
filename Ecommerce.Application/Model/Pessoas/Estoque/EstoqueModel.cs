

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
            string produto,
            int quantidadeAtual,
            DateTime dataUltimaMovimentacao
            )
        {
            UsuarioDocumento = usuarioDocumento;
            Usuario = usuario;
            Produto = produto;
            QuantidadeAtual = quantidadeAtual;
            DataUltimaMovimentacao = dataUltimaMovimentacao;
        }

        public string UsuarioDocumento { get; set; }
        public string Usuario { get; set; }
        public string Produto { get; set; }
        public int QuantidadeAtual { get; set;}
        public DateTime DataUltimaMovimentacao { get; set; }
    }
}
