using Dapper;
using Ecommerce.Domain.Entities.Pedidos;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Dapper.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Reflection;

namespace Ecommerce.Infra.Dapper.Repositories
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(
            IConfiguration configuration,
            IUnitOfWork unitOfWork)
            : base(configuration, unitOfWork)
        {
        }

        public override void Alterar(Pedido entidade)
        {
            throw new NotImplementedException();
        }

        public override void Cadastrar(Pedido entidade)
        {
            try
            {
                using var dbConnection = new SqlConnection(ConnectionString);

                var query = @"INSERT INTO PEDIDO (Usuario, UsuarioDocumento, Descricao, Quantidade, ValorUnitario, ValorTotal, DataPedido, TipoPedido, TipoPedidoDescricao, Status, StatusDescricao) 
                        values (@Usuario, @UsuarioDocumento, @Descricao, @Quantidade, @ValorUnitario, @ValorTotal, @DataPedido, @TipoPedido, @TipoPedidoDescricao, @Status, @StatusDescricao)";

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

                var query = @"DELETE FROM PEDIDO WHERE Id=@Id";

                dbConnection.Query(query, new { Id = id });
            }
            catch (Exception)
            {

                throw new Exception($"Erro ao {MethodBase.GetCurrentMethod()}");
            }
        }

        public override Pedido ObterPorId(int id)
        {
            try
            {
                using var dbConnection = new SqlConnection(ConnectionString);

                var query = @"SELECT * FROM PEDIDO WHERE Id= @Id";

                return dbConnection.Query<Pedido>(query, new { Id = id }).FirstOrDefault();
            }
            catch (Exception)
            {

                throw new Exception($"Erro ao {MethodBase.GetCurrentMethod()}");
            }
        }

        public override IList<Pedido> ObterTodos()
        {
            try
            {
                using var dbConnection = new SqlConnection(ConnectionString);

                var query = @"SELECT * FROM PEDIDO";

                return dbConnection.Query<Pedido>(query).ToList();
            }
            catch (Exception)
            {

                throw new Exception($"Erro ao {MethodBase.GetCurrentMethod()}");
            }
        }
    }
}
