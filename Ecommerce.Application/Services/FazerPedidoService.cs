using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Model;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Application.Services
{
    public class FazerPedidoService : IFazerPedidoService
    {
        private  IFazerPedidoDomainService _fazerPedidoDomainService;
        public FazerPedidoService()
        {
        }

        public FazerPedidoModel FazerPedido(FazerPedidoModel fazerPedidoModel)
        {
            var pedido = new FazerPedidoEntity
            {
                Id = 999,
                Usuario = "Teste",
                DataPedido = DateTime.Now,
                Status = 2,
                TipoPedido = 3
            };

            pedido = _fazerPedidoDomainService.FazerPedidoDomain(pedido);
            return fazerPedidoModel;
        }
    }
}
