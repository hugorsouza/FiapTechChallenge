using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Dapper.Services;

namespace Ecommerce.Infra.Dapper.Repositories
{
    public class PedidosRepositoryDapper : CrudBaseDapper<FazerPedidoEntity>, IFazerPedidoRepository
    {
        public PedidosRepositoryDapper(string connectionString) : base(connectionString)
        {  
        }

        public FazerPedidoEntity FazerPedido(FazerPedidoEntity fazerPedidoEntity)
        {
            throw new NotImplementedException();
        }
    }
}
