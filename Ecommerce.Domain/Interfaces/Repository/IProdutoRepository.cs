using Ecommerce.Domain.Entities.Estoque;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Interfaces.Repository;

namespace Ecommerce.Domain.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        void AdicionaUrlImagem(int idProduto, string diretorio);
        void DeletarUrlImagem(int idProduto);

        Task<int> CadastrarAsync(Produto produto, Estoque estoque);
    }
}
