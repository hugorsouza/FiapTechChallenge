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
using Ecommerce.Domain.Exceptions;
using System.Reflection;

namespace Ecommerce.Infra.Dapper.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(IConfiguration configuration, IUnitOfWork unitOfWork) : base(configuration, unitOfWork)
        {
        }

        public override void Cadastrar(Categoria entidade)
        {
            try
            {
                using var dbConnection = new SqlConnection(ConnectionString);

                var query = @"INSERT INTO CATEGORIA values (@Nome, @Ativo, @Descricao)";

                dbConnection.Query(query, entidade);
            }
            catch (Exception)
            {
                throw new Exception($"Erro ao {MethodBase.GetCurrentMethod()}");
            }

        }

        public override void Alterar(Categoria entidade)
        {
            try
            {
                using var dbConnection = new SqlConnection(ConnectionString);

                var query = @"UPDATE CATEGORIA SET Nome=@Nome, Ativo=@Ativo, Descricao=@Descricao Where Id=@Id";

                dbConnection.Query(query, entidade);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                throw new Exception($"Erro ao {MethodBase.GetCurrentMethod()}");
            }

        }

        public override void Deletar(int id)
        {
            throw new NotImplementedException();

        }

        public override Categoria ObterPorId(int id)
        {

            try
            {
                using var dbConnection = new SqlConnection(ConnectionString);

                var query = @"SELECT * FROM CATEGORIA WHERE Id=@Id";

                return dbConnection.Query<Categoria>(query, new { Id = id }).FirstOrDefault();
            }
            catch (Exception)
            {

                throw new Exception($"Erro ao {MethodBase.GetCurrentMethod()}");
            }

        }

        public override IList<Categoria> ObterTodos()
        {
            try
            {
                using var dbConnection = new SqlConnection(ConnectionString);

                var query = @"SELECT * FROM CATEGORIA";

                return dbConnection.Query<Categoria>(query).ToList();
            }
            catch (Exception)
            {

                throw new Exception($"Erro ao {MethodBase.GetCurrentMethod()}");
            }

        }
    }
}
