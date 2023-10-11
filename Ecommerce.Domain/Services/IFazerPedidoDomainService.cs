using Ecommerce.Domain.Entity;

namespace Ecommerce.Domain.Services
{
    public interface IFazerPedidoDomainService
    {
        public FazerPedidoEntity FazerPedido(FazerPedidoEntity fazerPedidoEntity);
    }
}
