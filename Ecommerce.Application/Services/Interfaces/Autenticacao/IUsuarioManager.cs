using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;

namespace Ecommerce.Application.Services.Interfaces.Autenticacao;

public interface IUsuarioManager
{
    Usuario CadastrarUsuario(Usuario usuario);
    Usuario AlterarSenha(Usuario usuario, string novaSenhaTextoPlano);
    Usuario CriarUsuarioParaCliente(CadastroClienteModel model);
    Usuario CriarUsuarioParaFuncionario(CadastroFuncionarioModel model);
}