using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entity
{
    public class Produto : Entidade
    {
        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Campo decimal Obrigatório")]
        public decimal Preco { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Descricao { get; set; }
        [Required]
        public int IdFabricante { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string UrlImagem { get; set; }


    }
}
