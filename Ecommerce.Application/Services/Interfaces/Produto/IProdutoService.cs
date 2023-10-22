using Ecommerce.Application.Model.Produto;
using Ecommerce.Domain.Entities.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Services
{
    public interface IProdutoService 
    {
        ProdutoViewModel Cadastrar(ProdutoViewModel entidade);
        Produto ObterPorId(int id);
        IList<Produto> ObterTodos();
        Produto Alterar(Produto entidade);
        void Deletar(int id);
    }
}
