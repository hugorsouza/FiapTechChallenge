using Ecommerce.Application.Model.Pessoas.Estoque;
using Ecommerce.Application.Model.Pessoas.Pedido;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Application.Services.Interfaces.Estoque;
using Ecommerce.Domain.Entities.Estoque;
using Ecommerce.Domain.Entities.Pedidos;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Domain.Repository;

namespace Ecommerce.Application.Services
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IUsuarioManager _usuarioManager;
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IEstoqueRepository _estoqueRepository;

        public EstoqueService(
            IUsuarioManager usuarioManager,
            IFuncionarioRepository funcionarioRepository,
            IEstoqueRepository estoqueRepository
            )
        {
            _usuarioManager = usuarioManager;
            _funcionarioRepository = funcionarioRepository;
            _estoqueRepository = estoqueRepository;
        }

        public EstoqueModel AlterarItemEstoque(int id, int quantidade)
        {
            var consultaUser = _usuarioManager.ObterUsuarioAtual();
            if (consultaUser == null)
                throw new Exception($"Funcionario {consultaUser.Id} não localizado");

            var consultaFuncionario = _funcionarioRepository.ObterPorId(consultaUser.Id);

            var estoque = new Estoque
            {
                Id = id,
                UsuarioDocumento = consultaFuncionario.Cpf,
                Usuario = consultaUser.NomeExibicao,
                QuantidadeAtual = quantidade,
                DataUltimaMovimentacao = DateTime.UtcNow
            };

            _estoqueRepository.Alterar(estoque);
            var pedido = _estoqueRepository.ObterPorId(id);
            return buildEstoqueModel(pedido);

        }

        public async Task<EstoqueModel> ObterItemEstoquePorId(int id)
        {
            var pedido = _estoqueRepository.ObterPorId(id);
            return buildEstoqueModel(pedido);
        }

        public async Task<IEnumerable<EstoqueModel>> ObterListaCompletaEstoque()
        {
            var pedido = _estoqueRepository.ObterTodos();
            return pedido.Select(x => buildEstoqueModel(x));
        }
        
        private EstoqueModel buildEstoqueModel(Estoque estoque)
        {
            if (estoque is null)
                return null;

            return new EstoqueModel(
                estoque.UsuarioDocumento,
                estoque.Usuario,
                estoque.Produto,
                estoque.QuantidadeAtual,
                estoque.DataUltimaMovimentacao
            );
        }
    }
}
