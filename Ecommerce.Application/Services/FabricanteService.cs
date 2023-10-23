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

            fabricante.CNPJ = fabricante.ObterCnpjSemFormatacao();

            if (!validaCNPJ(fabricante.CNPJ))
            //Lança Exceção

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

        public bool validaCNPJ(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
    }
}
