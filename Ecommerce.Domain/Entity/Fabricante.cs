using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entity
{
    public class Fabricante : Entidade
    {
        public string CNPJ { get; set; }
        public Endereco Endereco { get; set; }

    }
}
