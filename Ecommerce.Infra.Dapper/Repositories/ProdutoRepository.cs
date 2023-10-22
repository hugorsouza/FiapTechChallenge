using Dapper;
using Ecommerce.Domain.Entity;
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

namespace Ecommerce.Infra.Dapper.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(IConfiguration configuration, IUnitOfWork unitOfWork) : base(configuration, unitOfWork)
        {
        }

        public override void Cadastrar(Produto entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            var query = @"INSERT INTO PRODUTO values (@Nome, @Ativo, @Preco, @Descricao, @IdFabricante, @UrlImagem)";

            dbConnection.Query(query, entidade);
        }
        public override void Alterar(Produto entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            var query = @"UPDATE PRODUTO SET Nome=@Nome, Ativo=@Ativo, Preco=@Preco, Descricao=@Descricao, IdFabricante=@IdFabricante, UrlImagem=@UrlImagem WHERE Id=@Id";

            dbConnection.Query(query, entidade);
        }

       
        public override void Deletar(int id)
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            var query = @"DELETE FROM PRODUTO WHERE Id=@Id";

            dbConnection.Query(query, new {Id = id });
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
    }
}
