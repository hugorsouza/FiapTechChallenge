using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain.Entity;

namespace Ecommerce.Domain.Entities.Produtos
{
    public class Categoria : Entidade
    {
        public Categoria(string descricao, string nome, bool ativo, int id) : base()
        {
            Descricao = descricao;
            Nome = nome;
            Ativo = ativo;
            Id = id;
        }
        public Categoria()
        {

        }
        public Categoria(string descricao, string nome, bool ativo) : base()
        {
            Descricao = descricao;
            Nome = nome;
            Ativo= ativo;
        }



        public string Descricao { get; set; }

    }
}
