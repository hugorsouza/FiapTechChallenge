using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Domain.Entities.Pessoas.Fisica;

namespace Ecommerce.Application.Services.Interfaces.Pessoas;

public interface IFuncionarioService
{
    Task<FuncionarioViewModel> Cadastrar(CadastroFuncionarioModel model);
    Task<FuncionarioViewModel> ObterPorId(int id);
    Task<IEnumerable<FuncionarioViewModel>> ObterTodos();
    Task<FuncionarioViewModel> Alterar(AlterarFuncionarioModel model);
    FuncionarioViewModel BuildViewModel(Funcionario funcionario);
    Task<FuncionarioViewModel> Alterar(AlterarFuncionarioModel model, int usuarioId);
}