using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Interfaces
{
    public interface IFazerPedidoDomainService
    {
        public FazerPedidoEntity FazerPedido(FazerPedidoEntity fazerPedidoEntity);
    }
}
