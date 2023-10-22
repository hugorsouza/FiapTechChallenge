using Ecommerce.Application.Model.Produto;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Repository;
using Ecommerce.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class FabricanteService : IFabricanteService
    {

        private readonly IFabricanteRepository _fabricanteRepository;

        public FabricanteService(IFabricanteRepository fabricanteRepository)
        {
            _fabricanteRepository = fabricanteRepository;
        }

        public Fabricante Alterar(Fabricante entidade)
        {
            var categoria = ObterPorId(entidade.Id);

            if (categoria is null)
            {
                //Lançar erro
            }
            _fabricanteRepository.Alterar(entidade);
            
            return entidade;
        }

        public FabricanteViewModel Cadastrar(FabricanteViewModel model)
        {
            var fabricante = BuidFabricante(model);

            _fabricanteRepository.Cadastrar(fabricante);

            var fabricanteViewModel = BuildViewModel(fabricante);

            return fabricanteViewModel;
        }

        public  void Deletar(int id)
        {
            var fabricante = ObterPorId(id);

            if (fabricante is null)
            {
                //Lançar Erro
            }
            _fabricanteRepository.Deletar(id);
        }

        public  Fabricante ObterPorId(int id)
        {
            return _fabricanteRepository.ObterPorId(id);
        }

        public  IList<Fabricante> ObterTodos()
        {
            return _fabricanteRepository.ObterTodos();
        }

        private FabricanteViewModel BuildViewModel(Fabricante fabricante)
        {
            if (fabricante is null)
                return null;

            return new FabricanteViewModel(fabricante.Nome, fabricante.Ativo, fabricante.CNPJ, fabricante.Endereco);
        }

        private Fabricante BuidFabricante(FabricanteViewModel model)
        {
            if (model is null)
                return null;

            return new Fabricante(model.Nome,model.CNPJ, model.Ativo, model.Endereco);

        }
    }
}
