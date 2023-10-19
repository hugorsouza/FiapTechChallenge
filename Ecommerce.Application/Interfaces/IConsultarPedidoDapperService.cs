using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces
{
    public interface IConsultarPedidoDapperService
    {
        public ConsultarPedidoEntity ConsultarPedidoDomain(string usuario);
    }
}
