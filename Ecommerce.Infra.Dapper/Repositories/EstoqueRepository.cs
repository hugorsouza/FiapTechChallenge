using Dapper;
using Ecommerce.Domain.Entities.Estoque;
using Ecommerce.Domain.Entities.Pedidos;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Dapper.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Reflection;

namespace Ecommerce.Infra.Dapper.Repositories
{
    public class EstoqueRepository : Repository<Estoque>, IEstoqueRepository
    {
        public EstoqueRepository(
            IConfiguration configuration, 
            IUnitOfWork unitOfWork) 
            : base(configuration, unitOfWork)
        { 
        }

        public override void Alterar(Estoque entidade)
        {
            try
            {
                using var dbConnection = new SqlConnection(ConnectionString);

                    var query = @"UPDATE ESTOQUE
                                SET QuantidadeAtual = QuantidadeAtual + @QuantidadeAtual
                                WHERE ID = @id";

                dbConnection.Query(query, entidade);
            }
            catch (Exception)
            {

                throw new Exception($"Erro ao {MethodBase.GetCurrentMethod()}");
            }
        }

        public override void Cadastrar(Estoque entidade)
        {
            try
            {
                using var dbConnection = new SqlConnection(ConnectionString);

                var query = @"INSERT INTO ESTOQUE (Usuario, UsuarioDocumento, Produto, QuantidadeAtual, DataUltimaMovimentacao) 
                        values (@Usuario, @UsuarioDocumento, @Produto, @QuantidadeAtual, @DataUltimaMovimentacao)";

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

                var query = @"DELETE FROM ESTOQUE WHERE Id=@Id";

                dbConnection.Query(query, new { Id = id });
            }
            catch (Exception)
            {

                throw new Exception($"Erro ao {MethodBase.GetCurrentMethod()}");
            }
        }

        public override Estoque ObterPorId(int id)
        {
            try
            {
                using var dbConnection = new SqlConnection(ConnectionString);

                var query = @"SELECT * FROM ESTOQUE WHERE Id= @Id";

                return dbConnection.Query<Estoque>(query, new { Id = id }).FirstOrDefault();
            }
            catch (Exception)
            {

                throw new Exception($"Erro ao {MethodBase.GetCurrentMethod()}");
            }
        }

        public override IList<Estoque> ObterTodos()
        {
            try
            {
                using var dbConnection = new SqlConnection(ConnectionString);

                var query = @"SELECT * FROM ESTOQUE";

                return dbConnection.Query<Estoque>(query).ToList();
            }
            catch (Exception)
            {

                throw new Exception($"Erro ao {MethodBase.GetCurrentMethod()}");
            }
        }
    }
}
