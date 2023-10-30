using Ecommerce.Application.Model.Produto;
using Ecommerce.Domain.Entities.Produtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Services
{
    public interface IProdutoService 
    {
        Task<ProdutoViewModel> Cadastrar(ProdutoViewModel entidade);
        Produto ObterPorId(int id);
        IList<Produto> ObterTodos();
        Produto Alterar(Produto entidade);
        void Deletar(int id);
        Task<string> Upload(IFormFile arquivo, int id);
        Task DeletarimagemProduto(int id);

    }
}
