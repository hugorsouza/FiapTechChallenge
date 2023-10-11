using Ecommerce.Application.DTO;
using Ecommerce.Application.Model;

namespace Ecommerce.Application.Services.Interfaces
{
    public interface IFazerPedidoService
    {
        public FazerPedidoModel FazerPedido(FazerPedidoModel fazerPedidoDTO);
    }
}
