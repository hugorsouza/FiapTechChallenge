using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class FabricanteService : Service<Fabricante>, IFabricanteService
    {
        public override void Alterar(Fabricante entidade)
        {
            throw new NotImplementedException();
        }

        public override void Cadastrar(Fabricante entidade)
        {
            throw new NotImplementedException();
        }

        public override void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public override Fabricante ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        public override IList<Fabricante> ObterTodos()
        {
            throw new NotImplementedException();
        }
    }
}
