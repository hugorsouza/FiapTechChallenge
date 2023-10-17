using Ecommerce.Application.DTO;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces
{
    public interface IConsultarPedidoService
    {
        public ConsultarPedidoEntity ConsultarPedido(string usuario);
    }
}
