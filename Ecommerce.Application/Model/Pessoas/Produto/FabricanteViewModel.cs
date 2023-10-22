using Ecommerce.Domain.Entities.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Model.Produto
{
    public class FabricanteViewModel
    {
        public FabricanteViewModel(string nome, bool ativo, string cnpj, Endereco endereco)
        {
            Nome = nome;
            Ativo = ativo;
            CNPJ = cnpj;
            Endereco = endereco;
        }
        
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string CNPJ { get; set; }
        public Endereco Endereco { get; set; }
    }
}
