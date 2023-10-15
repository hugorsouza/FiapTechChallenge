using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Interfaces.Repository;

namespace Ecommerce.Domain.Services
{
    public class FazerPedidoDomainService : IFazerPedidoDomainService
    {
        private IFazerPedidoRepository _repository;
        public FazerPedidoDomainService(IFazerPedidoRepository fazerPedidoRepository)
        {
            _repository = fazerPedidoRepository;    
        }

        public FazerPedidoEntity FazerPedido(FazerPedidoEntity fazerPedidoEntity)
        {
            return _repository.FazerPedido(fazerPedidoEntity);
        }
    }
}
