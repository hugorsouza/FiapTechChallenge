using Dapper;
using Ecommerce.Domain.Repository;
using Ecommerce.Domain.Services;
using Ecommerce.Infra.Dapper.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Infra.Dapper.Interfaces;
using Ecommerce.Domain.Entities.Produtos;
using System.Reflection;
using Ecommerce.Domain.Entity;

namespace Ecommerce.Infra.Dapper.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(IConfiguration configuration, IUnitOfWork unitOfWork) : base(configuration, unitOfWork)
        {
        }

        public override void Cadastrar(Produto entidade)
        {

            try
            {
                using var dbConnection = new SqlConnection(ConnectionString);

                var query = @"INSERT INTO PRODUTO (Nome, Ativo, Preco, Descricao, FabricanteId, CategoriaId, UrlImagem) 
                        values (@Nome, @Ativo, @Preco, @Descricao, @FabricanteId, @CategoriaId, @UrlImagem)";

                dbConnection.Query(query, entidade);
            }
            catch (Exception)
            {

                throw new Exception($"Erro ao {MethodBase.GetCurrentMethod()}");
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
    }
}
