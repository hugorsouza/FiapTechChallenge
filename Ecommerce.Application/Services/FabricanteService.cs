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

        private readonly IFabricanteService _fabricanteService;

        public FabricanteService(IFabricanteService fabricanteService)
        {
            _fabricanteService = fabricanteService;
        }

        public override void Alterar(Fabricante entidade)
        {
            _fabricanteService.Alterar(entidade);
        }

        public override void Cadastrar(Fabricante entidade)
        {
            _fabricanteService.Cadastrar(entidade);
        }

        public override void Deletar(int id)
        {
            _fabricanteService.Deletar(id);
        }

        public override Fabricante ObterPorId(int id)
        {
            return _fabricanteService.ObterPorId(id);
        }

        public override IList<Fabricante> ObterTodos()
        {
            return _fabricanteService.ObterTodos();
        }
    }
}
