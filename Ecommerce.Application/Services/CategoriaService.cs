using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class CategoriaService : Service<Categoria>, ICategoriaService
    {
        public override void Alterar(Categoria entidade)
        {
            throw new NotImplementedException();
        }

        public override void Cadastrar(Categoria entidade)
        {
            throw new NotImplementedException();
        }

        public override void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public override Categoria ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        public override IList<Categoria> ObterTodos()
        {
            throw new NotImplementedException();
        }
    }
}
