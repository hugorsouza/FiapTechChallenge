using Ecommerce.Domain.Entities.Pessoas.Autenticacao;

namespace Ecommerce.Domain.Interfaces.Repository;

public interface IUsuarioRepository : IRepository<Usuario>
{
    IList<Usuario> ObterTodos();
    void Alterar(Usuario entidade);
    void AlterarSenha(Usuario entidade);
    void Deletar(int id);
    Usuario ObterPorId(int id);
    Usuario ObterUsuarioPorEmail(string email);
    void Cadastrar(Usuario entidade);
    int CadastrarObterId(Usuario entidade);
}