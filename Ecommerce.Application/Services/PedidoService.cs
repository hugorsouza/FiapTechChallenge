using Ecommerce.Application.Model.Pessoas.Pedido;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Application.Services.Interfaces.Pedido;
using Ecommerce.Domain.Entities.Pedidos;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Domain.Repository;

namespace Ecommerce.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IUsuarioManager _usuarioManager;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public PedidoService(
            IPedidoRepository pedidoRepository,
            IProdutoRepository produtoRepository,
            IClienteRepository clienteRepository,
            IUsuarioManager usuarioManager,
            IFuncionarioRepository funcionarioRepository
            )
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
            _clienteRepository = clienteRepository;
            _usuarioManager = usuarioManager;
            _funcionarioRepository = funcionarioRepository;
        }

        public Task<PedidoModel> AlterarPedido(int idPedido)
        {
            //var pedido = ObterPorId( idPedido );
            // if (pedido is null)
            //     throw new ArgumentException($"Erro: O pedido {idPedido} não está cadastrado na Base");
            // _pedidoRepository.Alterar(pedido);
            // return pedido;
            throw new NotImplementedException();
        }

        public PedidoModel CadastrarPedido(int produtoId, int quantidade)
        {
            var consultaUser = _usuarioManager.ObterUsuarioAtual();
            if (consultaUser == null)
                throw new Exception($"Usuario ID não localizado");

            var consultaProduto = _produtoRepository.ObterPorId(produtoId);
            if (consultaProduto == null)
                throw new Exception($"ProdutoId {produtoId} não localizado");

            var consultaCliente = _clienteRepository.ObterPorId(consultaUser.Id);
            var consultaFuncionario = _funcionarioRepository.ObterPorId(consultaUser.Id);

            if (consultaFuncionario == null && consultaCliente == null)
                throw new Exception($"ClienteId ou FuncionarioId {consultaUser.Id} não localizado");

            var documento = "";
            if(consultaCliente == null)
            {
                documento = consultaFuncionario.Cpf;
            }
            else
            {
                documento = consultaCliente.Cpf;
            }

            var pedido = new Pedido
            {
                UsuarioDocumento = documento,
                Usuario = consultaUser.NomeExibicao,
                Descricao = consultaProduto.Descricao,
                Quantidade = quantidade,
                ValorUnitario = consultaProduto.Preco,
                ValorTotal = consultaProduto.Preco * quantidade,
                DataPedido = DateTime.UtcNow,
                TipoPedido = (int)consultaUser.Perfil,
                TipoPedidoDescricao = consultaUser.Perfil.ToString(),
                Status = 1,
                StatusDescricao = "Descrição teste"
            };

            _pedidoRepository.Cadastrar(pedido);
            var pedidoModel = buildPedidoModel(pedido);
            return pedidoModel;
        }

        public void DeletarPedido(int idPedido)
        {
            var pedido = ObterPedidoPorId(idPedido);
            if (pedido is null)
            {
                throw new ArgumentException($"Erro: O Produto {idPedido} não está cadastrado na Base");
            }
            _pedidoRepository.Deletar(idPedido);
        }

        public async Task<PedidoModel> ObterPedidoPorId(int id)
        {
            var pedido = _pedidoRepository.ObterPorId(id);
            return buildPedidoModel(pedido);
        }

        public async Task<IEnumerable<PedidoModel>> ObterTodosPedido()
        {
            var pedido = _pedidoRepository.ObterTodos();
            return pedido.Select(x => buildPedidoModel(x));
        }

        private PedidoModel buildPedidoModel(Pedido pedido)
        {
            if (pedido is null)
                return null;

            return new PedidoModel(
                pedido.UsuarioDocumento,
                pedido.Usuario,
                pedido.Descricao,
                pedido.Quantidade,
                pedido.ValorUnitario,
                pedido.ValorTotal,
                pedido.DataPedido,
                pedido.TipoPedido,
                pedido.TipoPedidoDescricao,
                pedido.Status,
                pedido.StatusDescricao
            );
        }
    }
}
