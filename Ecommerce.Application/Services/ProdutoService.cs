using Ecommerce.Application.Model.Produto;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Repository;
using Ecommerce.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class ProdutoService : IProdutoService
    {

        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public ProdutoViewModel Cadastrar(ProdutoViewModel entidade)
        {
            var produto = buidProduto(entidade);

            var produtos = ObterTodos().ToList();

            if (produtos.Any(x => x.Nome.Equals(produto.Nome)))
                //Lança Exceção
                
            _produtoRepository.Cadastrar(produto);

            var produtoViewModel = BuildViewModel(produto);

            return produtoViewModel;

        }

        public Produto Alterar(Produto entidade)
        {
            var produto = ObterPorId(entidade.Id);
            if (produto is null)
            {
                //Lançar Erro
            }

            _produtoRepository.Alterar(entidade);

            return entidade;



        }     
        public void Deletar(int id)
        {
            var produto = ObterPorId(id);
            if (produto is null)
            {
                //Lançar Erro
            }
            _produtoRepository.Deletar(id);
        }

        public Produto ObterPorId(int id)
        {
           return  _produtoRepository.ObterPorId(id);
        }

        public IList<Produto> ObterTodos()
        {
            return _produtoRepository.ObterTodos();
        }

        private ProdutoViewModel BuildViewModel(Produto produto)
        {
            if (produto is null)
                return null;

            return new ProdutoViewModel(produto.Ativo, produto.Nome,produto.Preco ,
                produto.Descricao, produto.FabricanteId, produto.UrlImagem, produto.CategoriaId);
        }

        private Produto buidProduto(ProdutoViewModel model)
        {
            if (model is null)
                return null;

            return new Produto();

        }
    }
}
