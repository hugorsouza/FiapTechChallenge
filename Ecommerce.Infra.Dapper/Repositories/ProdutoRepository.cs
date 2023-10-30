using Dapper;
using Ecommerce.Domain.Repository;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Ecommerce.Infra.Dapper.Interfaces;
using Ecommerce.Domain.Entities.Produtos;
using System.Reflection;
using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Entities.Estoque;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ecommerce.Infra.Dapper.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(IConfiguration configuration, IUnitOfWork unitOfWork) : base(configuration, unitOfWork)
        {
        }

        public async Task<int?> CadastrarAsync(Produto produto, Estoque estoque)
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
                try
                {
                    transaction.Rollback();
                    return null;

                }
                catch (Exception)
                {
                    throw new Exception($"Erro ao {MethodBase.GetCurrentMethod()}");
                }

                
            }
            
        }
        public override void Alterar(Produto entidade)
        {
            try
            {
                using var dbConnection = new SqlConnection(ConnectionString);

                var query = @"UPDATE PRODUTO SET Nome=@Nome, Ativo=@Ativo, Preco=@Preco, Descricao=@Descricao,CategoriaId=@CategoriaId, FabricanteId=@FabricanteId WHERE Id=@Id";

                dbConnection.Query(query, entidade);
            }
            catch (Exception)
            {

                throw new Exception($"Erro ao {MethodBase.GetCurrentMethod()}");
            }

        }

       
        public override void Deletar(int id)
        {
            try
            {
                using var dbConnection = new SqlConnection(ConnectionString);

                var query = @"DELETE FROM PRODUTO WHERE Id=@Id";

                dbConnection.Query(query, new { Id = id });
            }
            catch (Exception)
            {

                throw new Exception($"Erro ao {MethodBase.GetCurrentMethod()}");
            }

        }

        public override Produto ObterPorId(int id)
        {
            try
            {
                using var dbConnection = new SqlConnection(ConnectionString);

                var query = @"SELECT * FROM PRODUTO WHERE Id=@Id";

                return dbConnection.Query<Produto>(query, new { Id = id }).FirstOrDefault();
            }
            catch (Exception)
            {

                throw new Exception($"Erro ao {MethodBase.GetCurrentMethod()}");
            }
        }

        public override IList<Produto> ObterTodos()
        {
            try
            {
                using var dbConnection = new SqlConnection(ConnectionString);

                var query = @"SELECT * FROM PRODUTO";

                return dbConnection.Query<Produto>(query).ToList();
            }
            catch (Exception)
            {

                throw new Exception($"Erro ao {MethodBase.GetCurrentMethod()}");
            }

        }

        public void AdicionaUrlImagem(int idProduto, string diretorio)
        {
            try
            {
                using var dbConnection = new SqlConnection(ConnectionString);

                var query = @"UPDATE PRODUTO SET UrlImagem=@UrlImagem WHERE Id=@Id";

                dbConnection.Query(query, new {Id= idProduto, UrlImagem= diretorio});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception($"Erro ao {MethodBase.GetCurrentMethod()} do produto {idProduto}");
            }
        }


        public void DeletarUrlImagem(int idProduto)
        {
            try
            {
                using var dbConnection = new SqlConnection(ConnectionString);

                var query = @"UPDATE PRODUTO SET UrlImagem=null WHERE Id=@Id";

                dbConnection.Query(query, new { Id = idProduto});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception($"Erro ao {MethodBase.GetCurrentMethod()} do produto {idProduto}");
            }
        }

        public override void Cadastrar(Produto entidade)
        {
            throw new NotImplementedException();
        }
    }
}
