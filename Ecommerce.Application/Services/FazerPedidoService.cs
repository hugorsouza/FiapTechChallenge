using Ecommerce.Application.DTO;
using Ecommerce.Application.Model;
using Ecommerce.Application.Services.Interfaces;

namespace Ecommerce.Application.Services
{
    public class FazerPedidoService : IFazerPedidoService
    {
        private readonly IFazerPedidoService _fazerPedidoService;
        public FazerPedidoService()
        {
        }

        public FazerPedidoModel FazerPedido(FazerPedidoModel fazerPedidoModel)
        {
            var fazerPedido = new FazerPedidoModel
            {
                DataPedido = DateTime.Now,
                Id = 999,
                Status = 0,
                TipoPedido = 1,
                Usuario = "Teste"
            };
            return fazerPedido;
        }
    }
}
