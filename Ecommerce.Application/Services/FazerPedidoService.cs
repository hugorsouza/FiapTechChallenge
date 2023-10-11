using Ecommerce.Application.DTO;
using Ecommerce.Application.Services.Interfaces;

namespace Ecommerce.Application.Services
{
    public class FazerPedidoService : IFazerPedidoService
    {
        private readonly IFazerPedidoService _fazerPedidoService;
        public FazerPedidoService()
        {
        }
        public FazerPedidoDTO FazerPedido(FazerPedidoDTO fazerPedidoDTO)
        {
            //var fazerPedido = _fazerPedidoService.FazerPedido(fazerPedidoDTO); 
            return fazerPedidoDTO;
        }
    }
}
