using Azure.Storage.Blobs;
using Ecommerce.Application.Model.Produto;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Repository;
using Ecommerce.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace Ecommerce.Application.Services
{
    public class ProdutoService : IProdutoService
    {

        private readonly IProdutoRepository _produtoRepository;
        private readonly IConfiguration _configuration;

        public ProdutoService(IProdutoRepository produtoRepository, IConfiguration configuration)
        {
            _produtoRepository = produtoRepository;
            _configuration = configuration;
        }

        public ProdutoViewModel Cadastrar(ProdutoViewModel entidade)
        {
            var produto = buidProduto(entidade);

            if (ObterTodos().Where(x => x.Nome != null)
                    .Any(x => x.Nome.Equals(produto.Nome)))
                throw new ArgumentException($"Erro: O Produto {produto.Nome} já está cadastrado!");

            _produtoRepository.Cadastrar(produto);

            var produtoViewModel = BuildViewModel(produto);

            return produtoViewModel;

        }

        public Produto Alterar(Produto entidade)
        {
            var produto = ObterPorId(entidade.Id);
            if (produto is null)
                throw new ArgumentException($"Erro: O Produto {entidade.Id} não está cadastrado na Base");

            _produtoRepository.Alterar(entidade);

            return entidade;



        }
        public void Deletar(int id)
        {
            var produto = ObterPorId(id);
            if (produto is null)
            {
                throw new ArgumentException($"Erro: O Produto {id} não está cadastrado na Base");
            }
            _produtoRepository.Deletar(id);
        }

        public Produto ObterPorId(int id)
        {
            return _produtoRepository.ObterPorId(id);
        }

        public IList<Produto> ObterTodos()
        {
            return _produtoRepository.ObterTodos();
        }

        private ProdutoViewModel BuildViewModel(Produto produto)
        {
            if (produto is null)
                return null;

            return new ProdutoViewModel(produto.Ativo, produto.Nome, produto.Preco,
                produto.Descricao, produto.FabricanteId, produto.UrlImagem, produto.CategoriaId);
        }

        private Produto buidProduto(ProdutoViewModel model)
        {
            if (model is null)
                return null;

            return new Produto(model.Ativo, model.Nome, model.Preco,
                model.Descricao, model.FabricanteId, model.UrlImagem, model.CategoriaId);

        }

        public async Task<string> Upload(IFormFile arquivoimportado, int idProduto)
        {
            string diretorio;
            
            if (!ObterTodos().Any(x => x.Id == idProduto))
                throw new ArgumentException($"Erro: O Produto Id={idProduto} não estã cadastrado, por isso não será possivel carregar a imagem");

            if(arquivoimportado==null)
                throw new ArgumentException($"Erro: Imagem não anexada para carregar a imagem do Produto Id={idProduto}");

            try
            {
                string nomeArquivo = (idProduto.ToString() + ".jpg");

                using var stream = new MemoryStream();
                arquivoimportado.CopyTo(stream);
                stream.Position = 0;


                var connection = _configuration.GetSection("BlobStorage:ConectionString").Value;
                var containnerName = _configuration.GetSection("BlobStorage:Container:ImagemProduto").Value;

                BlobContainerClient container = new BlobContainerClient(connection, containnerName);

                BlobClient blob = container.GetBlobClient(nomeArquivo);

            
                await blob.DeleteIfExistsAsync();
                
                await blob.UploadAsync(stream);
                


                diretorio = blob.Uri.AbsoluteUri.ToString();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Erro: Erro para carregar o Produto {idProduto} no diretório");
            }


            if (diretorio != null)
            {
                _produtoRepository.AdicionaUrlImagem(idProduto, diretorio);
            }

            return diretorio.ToString();
        }

        public async Task DeletarimagemProduto(int idProduto)
        {
           

            if (!ObterTodos().Any(x => x.Id == idProduto))
                throw new ArgumentException($"Erro: O Produto Id={idProduto} não estã cadastrado, por isso não será possivel carregar a imagem");


            try
            {
                string nomeArquivo = (idProduto.ToString() + ".jpg");



                var connection = _configuration.GetSection("BlobStorage:ConectionString").Value;
                var containnerName = _configuration.GetSection("BlobStorage:Container:ImagemProduto").Value;

                BlobContainerClient container = new BlobContainerClient(connection, containnerName);

                BlobClient blob = container.GetBlobClient(nomeArquivo);

                await blob.DeleteIfExistsAsync();

                
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Erro: Erro para Deletar a imagem do Produto {idProduto} no diretório");
            }

            
            _produtoRepository.DeletarUrlImagem(idProduto);
            
        }
    }
}
