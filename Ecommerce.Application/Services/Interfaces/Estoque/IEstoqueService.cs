using Ecommerce.Application.Model.Pessoas.Estoque;
using Ecommerce.Application.Model.Pessoas.Pedido;

namespace Ecommerce.Application.Services.Interfaces.Estoque
{
    public interface IEstoqueService
    {
        EstoqueModel AlterarItemEstoque(int id, int quantidade);
        Task<EstoqueModel> ObterItemEstoquePorId(int id);
        Task<IEnumerable<EstoqueModel>> ObterListaCompletaEstoque();
    }
}
