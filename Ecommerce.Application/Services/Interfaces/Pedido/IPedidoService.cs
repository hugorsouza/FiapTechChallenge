using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Application.Model.Pessoas.Pedido;
using Ecommerce.Domain.Entities.Pedidos;

namespace Ecommerce.Application.Services.Interfaces.Pedido
{
    public interface IPedidoService
    {
        PedidoModel CadastrarPedido(int produtoId, int quantidade);
        Task<PedidoModel> ObterPedidoPorId(int id);
        Task<IEnumerable<PedidoModel>> ObterTodosPedido();
        Task<PedidoModel> AlterarPedido(int idPedido);
        void DeletarPedido(int idPedido);
    }
}
