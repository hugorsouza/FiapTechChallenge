using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Domain.Services
{
    public class FazerPedidoDomainService : IFazerPedidoDomainService
    {
        private IFazerPedidoRepository _repository;
        
        public FazerPedidoDomainService(IFazerPedidoRepository fazerPedidoRepository)
        {
            _repository = fazerPedidoRepository;  
        }

        public FazerPedidoEntity FazerPedidoDomain(FazerPedidoEntity fazerPedidoEntity)
        {
            return _repository.FazerPedidoDomain(fazerPedidoEntity);
        }
    }
}
