using Ecommerce.Domain.Entities;

namespace Ecommerce.Infra.Dapper.Interfaces
{
    public interface IConsultarPedidoRepositoryDapper
    {
        public ConsultarPedidoEntity ConsultarPedidoDomain(string usuario);
    }
}
