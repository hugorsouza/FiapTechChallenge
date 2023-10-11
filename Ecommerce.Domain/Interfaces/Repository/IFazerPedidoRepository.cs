using Ecommerce.Domain.Entity;

namespace Ecommerce.Domain.Interfaces.Repository
{
    public interface IFazerPedidoRepository
    {
        public FazerPedidoEntity FazerPedido(FazerPedidoEntity fazerPedidoEntity);
    }
}
