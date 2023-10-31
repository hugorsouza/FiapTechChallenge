using Ecommerce.Domain.Entities.Produtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Model.Produto
{
    public class ProdutoViewModel
    {

        public ProdutoViewModel(bool ativo, string nome, decimal preco, string descricao,int fabricanteId, string urlImagem, int categoriaId)
        {
            Ativo = ativo;
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            FabricanteId = fabricanteId;
            UrlImagem = urlImagem;
            CategoriaId = categoriaId;


        } 
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public int FabricanteId { get; set; }
        public string UrlImagem { get; set; }
        public int CategoriaId { get; set; }
    }
}
