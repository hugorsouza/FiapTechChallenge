using Ecommerce.Domain.Entity;

namespace Ecommerce.Domain.Interfaces.Services
{
    public interface IFazerPedidoDomainService
    {
        public FazerPedidoEntity FazerPedido(FazerPedidoEntity fazerPedidoEntity);
    }
}
