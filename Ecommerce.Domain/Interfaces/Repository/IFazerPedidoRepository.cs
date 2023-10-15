using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Interfaces.Repository
{
    public interface IFazerPedidoRepository
    {
        public FazerPedidoEntity FazerPedido(FazerPedidoEntity fazerPedidoEntity);
    }
}
