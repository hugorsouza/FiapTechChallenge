using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entity
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public int IdEntidade { get; set; }
    }
}
