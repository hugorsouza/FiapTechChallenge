using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Domain.Entities.Pessoas.Fisica;

namespace Ecommerce.Application.Services.Interfaces.Pessoas;

public interface IClienteService
{
    Task<ClienteViewModel> Cadastrar(CadastroClienteModel model);
    Task<ClienteViewModel> ObterPorId(int id);
    Task<IEnumerable<ClienteViewModel>> ObterTodos();
    ClienteViewModel BuildViewModel(Cliente cliente);
    Task<ClienteViewModel> Alterar(AlterarClienteModel model);
    Task Desativar(int clienteId);
}