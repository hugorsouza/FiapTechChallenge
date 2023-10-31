
using Ecommerce.Application.Model.Produto;
using Ecommerce.Domain.Entities.Produtos;

namespace Ecommerce.Domain.Services
{
    public interface ICategoriaService
    {
        CategoriaViewModel Cadastrar(CategoriaViewModel entidade);
        Categoria ObterPorId(int id);
        IList<Categoria> ObterTodos();
        Categoria Alterar(Categoria entidade);
    }
}
