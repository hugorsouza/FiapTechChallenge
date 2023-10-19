using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Infra.Dapper.Interfaces;

namespace Ecommerce.Application.Services
{
    public class ConsultarPedidoDapperService : IConsultarPedidoDapperService
    {
        private IConsultarPedidoRepositoryDapper _consultarPedidoRepositoryDapper;

        public ConsultarPedidoDapperService(IConsultarPedidoRepositoryDapper consultarPedidoRepositoryDapper)
        {
            _consultarPedidoRepositoryDapper = consultarPedidoRepositoryDapper;
        }
        public ConsultarPedidoEntity ConsultarPedidoDomain (string usuario) 
        {
            return _consultarPedidoRepositoryDapper.ConsultarPedidoDomain(usuario);
        }
    }
}
