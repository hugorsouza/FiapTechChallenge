using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        void AdicionaUrlImagem(int idProduto, string diretorio);
        void DeletarUrlImagem(int idProduto);
    }
}
