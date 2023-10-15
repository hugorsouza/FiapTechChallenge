using Ecommerce.Domain.Entity.Autenticacao;

namespace Ecommerce.Domain.Interfaces.Repository;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterUsuarioPorId(int id);

    Task<Usuario?> ObterUsuarioPorEmail(string email);

    Task<Usuario> Inserir(Usuario usuario);

    Task<Usuario?> Update(Usuario usuario);
}