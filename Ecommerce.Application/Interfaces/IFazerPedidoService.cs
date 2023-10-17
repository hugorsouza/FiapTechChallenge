using Ecommerce.Application.Model;

namespace Ecommerce.Application.Interfaces
{
    public interface IFazerPedidoService
    {
        public FazerPedidoModel FazerPedido(FazerPedidoModel fazerPedidoModel);
    }
}
