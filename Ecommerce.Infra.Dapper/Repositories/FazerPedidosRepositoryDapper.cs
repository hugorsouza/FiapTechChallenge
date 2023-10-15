using Dapper;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Dapper.DataBase.Queries;
using Ecommerce.Infra.Dapper.Services;

namespace Ecommerce.Infra.Dapper.Repositories
{
    public class FazerPedidosRepositoryDapper : CrudBaseDapper<FazerPedidoEntity>, IFazerPedidoRepository
    {
        public FazerPedidosRepositoryDapper(string connectionString) : base(connectionString)
        {  
        }

        public FazerPedidoEntity FazerPedido(FazerPedidoEntity fazerPedidoEntity)
        {
            string script = AllQueries.FazerPedido;
            var param = new DynamicParameters();
            var tamanhoDocumento = fazerPedidoEntity.Usuario.Length;

            param.Add("@Id", fazerPedidoEntity.Id, null);
            param.Add("@Usuario", fazerPedidoEntity.Usuario, System.Data.DbType.AnsiStringFixedLength, null, tamanhoDocumento);
            param.Add("@DataPedido", fazerPedidoEntity.DataPedido = DateTime.Now, null);
            param.Add("@TipoPedido", fazerPedidoEntity.TipoPedido, null);
            param.Add("@Status", fazerPedidoEntity.Status, null);

            fazerPedidoEntity.Id = ExecuteScriptWithoutTransactionNoList<int>(script, param);

            return fazerPedidoEntity;
        }
    }
}
