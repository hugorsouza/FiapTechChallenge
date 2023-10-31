using Ecommerce.Application.Model.Produto;
using Ecommerce.Domain.Entities.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Services
{
    public interface IFabricanteService
    {
        FabricanteViewModel Cadastrar(FabricanteViewModel entidade);
        Fabricante ObterPorId(int id);
        IList<Fabricante> ObterTodos();
        Fabricante Alterar(Fabricante entidade);
        void Deletar(int id);

    }
}
