using Ecommerce.Domain.Entity;

namespace Ecommerce.Domain.Interfaces
{
    public interface IFazerPedidoDomainService
    {
        public FazerPedidoEntity FazerPedido(FazerPedidoEntity fazerPedidoEntity);
    }
}
