using Ecommerce.Application.DTO;

namespace Ecommerce.Infra.Dapper.Interfaces
{
    public interface IFazerPedidoRepositoryDapper
    {
        void FazerPedido(FazerPedidoDTO fazerPedidoDTO);
    }
}
