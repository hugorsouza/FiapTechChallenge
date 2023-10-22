using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain.Entity;

namespace Ecommerce.Domain.Entities.Produtos
{
    public class Fornecedor : Entidade
    {
        public string CNPJ { get; set; }
        public Endereco Endereco { get; set; }


    }
}
