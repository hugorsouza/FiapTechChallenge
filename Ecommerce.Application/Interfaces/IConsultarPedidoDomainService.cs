using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces
{
    public interface IConsultarPedidoDomainService
    {
        public ConsultarPedidoEntity ConsultarPedidoDomain(string usuario);
    }
}
