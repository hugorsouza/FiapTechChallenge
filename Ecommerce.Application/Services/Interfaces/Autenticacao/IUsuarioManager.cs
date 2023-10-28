using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Application.Model.Pessoas.Autenticacao;
using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;

namespace Ecommerce.Application.Services.Interfaces.Autenticacao;

public interface IUsuarioManager
{
    Usuario CadastrarUsuario(Usuario usuario);
    Usuario Alterar(Usuario usuario);
    void AlterarSenha(AlterarSenhaModel model);
    void AlterarSenha(AlterarSenhaModel model, Usuario usuario);
    Usuario ObterUsuarioAtual();
    string? ObterEmailUsuarioAtual();
    Usuario ObterUsuarioPorEmail(string email);
    Usuario BuildUsuarioParaCliente(CadastroClienteModel model);
    Usuario BuildUsuarioParaFuncionario(CadastroFuncionarioModel model);
    Usuario ObterPorId(int id);
    Task<bool> SouAdministrador();
}