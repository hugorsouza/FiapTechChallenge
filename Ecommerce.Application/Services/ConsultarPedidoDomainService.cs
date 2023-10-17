using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Registration;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Infra.Dapper.Interfaces;

namespace Ecommerce.Application.Services
{
    public class ConsultarPedidoDomainService : IConsultarPedidoDomainService
    {
        private IConsultarPedidoRepositoryDapper _consultarPedidoRepositoryDapper;

        public ConsultarPedidoDomainService(IConsultarPedidoRepositoryDapper consultarPedidoRepositoryDapper)
        {
            _consultarPedidoRepositoryDapper = consultarPedidoRepositoryDapper;
        }
        public ConsultarPedidoEntity ConsultarPedidoDomain (string usuario) 
        {
            return _consultarPedidoRepositoryDapper.ConsultarPedidoDomain(usuario);
        }
    }
}
