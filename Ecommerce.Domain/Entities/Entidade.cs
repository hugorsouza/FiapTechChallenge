using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entity
{
    public class Entidade
    {
        
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

    }
}
