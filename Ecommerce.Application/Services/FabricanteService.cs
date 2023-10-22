using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Repository;
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

        private readonly IFabricanteRepository _fabricanteRepository;

        public FabricanteService(IFabricanteRepository fabricanteRepository)
        {
            _fabricanteRepository = fabricanteRepository;
        }

        public override void Alterar(Fabricante entidade)
        {
            _fabricanteRepository.Alterar(entidade);
        }

        public override void Cadastrar(Fabricante entidade)
        {
            _fabricanteRepository.Cadastrar(entidade);
        }

        public override void Deletar(int id)
        {
            _fabricanteRepository.Deletar(id);
        }

        public override Fabricante ObterPorId(int id)
        {
            return _fabricanteRepository.ObterPorId(id);
        }

        public override IList<Fabricante> ObterTodos()
        {
            return _fabricanteRepository.ObterTodos();
        }
    }
}
