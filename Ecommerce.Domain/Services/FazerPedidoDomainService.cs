using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Domain.Interfaces.Services;

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
