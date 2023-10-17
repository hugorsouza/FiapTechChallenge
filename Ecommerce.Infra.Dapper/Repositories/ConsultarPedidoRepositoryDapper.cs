using Dapper;
using Ecommerce.Domain.Entities;
using Ecommerce.Infra.Dapper.DataBase.Queries;
using Ecommerce.Infra.Dapper.Interfaces;
using Ecommerce.Infra.Dapper.Services;

namespace Ecommerce.Infra.Dapper.Repositories
{
    public class ConsultarPedidoRepositoryDapper : CrudBaseDapper<ConsultarPedidoEntity>, IConsultarPedidoRepositoryDapper
    {
        public ConsultarPedidoRepositoryDapper(string connectionString) : base(connectionString)
        {
        }

        public ConsultarPedidoEntity ConsultarPedidoDomain(string usuario)
        {
            string script = AllQueries.ConsultarPedido;
            var param = new DynamicParameters();

            param.Add("@Id", usuario, null);

           return ExecuteScriptWithTransactionNoList<ConsultarPedidoEntity>(script, param);
        }
    }
}
