using Dapper;
using Ecommerce.Domain.Repository;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Ecommerce.Infra.Dapper.Interfaces;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Entities.Estoque;

namespace Ecommerce.Infra.Dapper.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(IConfiguration configuration, IUnitOfWork unitOfWork) : base(configuration, unitOfWork)
        {
        }

        public async Task<int> CadastrarAsync(Produto produto, Estoque estoque)
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            dbConnection.Open();
            var transaction = dbConnection.BeginTransaction();

            try
            {                

                var query1 = @"INSERT INTO PRODUTO (Nome, Ativo, Preco, Descricao, FabricanteId, CategoriaId, UrlImagem) 
                        values (@Nome, @Ativo, @Preco, @Descricao, @FabricanteId, @CategoriaId, @UrlImagem);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
                

                var result = await dbConnection.QueryFirstOrDefaultAsync<int>(query1, produto, transaction);

                estoque.ProdutoId= result;

                var query2 = @"INSERT INTO ESTOQUE (Usuario, UsuarioDocumento, Produto, QuantidadeAtual, DataUltimaMovimentacao) 
                        values (@Usuario, @UsuarioDocumento, @ProdutoId, @QuantidadeAtual, @DataUltimaMovimentacao)";

                dbConnection.Query(query2, estoque, transaction);

                transaction.Commit();

                return result;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public override void Alterar(Produto entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            var query = @"UPDATE PRODUTO SET Nome=@Nome, Ativo=@Ativo, Preco=@Preco, Descricao=@Descricao,CategoriaId=@CategoriaId, FabricanteId=@FabricanteId WHERE Id=@Id";

            dbConnection.Query(query, entidade);
        }

       
        public override void Deletar(int id)
        {
            throw new NotImplementedException();

        }

        public override Produto ObterPorId(int id)
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            var query = @"SELECT * FROM PRODUTO WHERE Id=@Id";

            return dbConnection.Query<Produto>(query, new { Id = id }).FirstOrDefault();
        }

        public override IList<Produto> ObterTodos()
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            var query = @"SELECT * FROM PRODUTO";

            return dbConnection.Query<Produto>(query).ToList();
        }

        public void AdicionaUrlImagem(int idProduto, string diretorio)
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            var query = @"UPDATE PRODUTO SET UrlImagem=@UrlImagem WHERE Id=@Id";

            dbConnection.Query(query, new { Id = idProduto, UrlImagem = diretorio });
        }


        public void DeletarUrlImagem(int idProduto)
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            var query = @"UPDATE PRODUTO SET UrlImagem=null WHERE Id=@Id";

            dbConnection.Query(query, new { Id = idProduto });
        }

        public override void Cadastrar(Produto entidade)
        {
            throw new NotImplementedException();
        }
    }
}
