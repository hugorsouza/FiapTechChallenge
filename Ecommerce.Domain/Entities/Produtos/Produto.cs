using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain.Entity;

namespace Ecommerce.Domain.Entities.Produtos
{
    public class Produto : Entidade
    {
        public Produto()
        {

        }

        public Produto(bool ativo, string nome, decimal preco,
            string descricao, int fabricanteId, string urlImagem, int categoriaId)
        {
            Ativo = ativo;
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            FabricanteId = fabricanteId;
            UrlImagem = urlImagem;
            CategoriaId = categoriaId;

        }
        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Campo decimal Obrigatório")]
        public decimal Preco { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Descricao { get; set; }
        [Required]
        public int FabricanteId { get; set; }
        public string UrlImagem { get; set; }
        public int CategoriaId { get; set; }



    }
}
