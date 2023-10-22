using Ecommerce.Domain.Entities.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Model.Produto
{
    public class CategoriaViewModel
    {
        public CategoriaViewModel(string nome, string descricao, bool ativo)
        {
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}
