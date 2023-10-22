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

namespace Ecommerce.Infra.Dapper.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Cadastrar(Categoria entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            var query = @"INSERT INTO CATEGORIA values (@Nome, @Ativo, @Descricao)";

            dbConnection.Query(query, entidade);
        }

        public override void Alterar(Categoria entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            var query = @"UPDATE CATEGORIA SET Nome=@Nome, Ativo=@Ativo, Descricao=@Descricao WHERE Id=@Id";

            dbConnection.Query(query, entidade);
        }

        public override void Deletar(int id)
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            var query = @"DELETE FROM CATEGORIA WHERE Id=@Id";

            dbConnection.Query(query, new { Id = id });
        }

        public override Categoria ObterPorId(int id)
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            var query = @"SELECT * FROM CATEGORIA WHERE Id=@Id";

            return dbConnection.Query<Categoria>(query, new { Id = id }).FirstOrDefault();
        }

        public override IList<Categoria> ObterTodos()
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            var query = @"SELECT * FROM CATEGORIA";

            return dbConnection.Query<Categoria>(query).ToList();
        }
    }
}
