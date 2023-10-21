using Ecommerce.Domain.Entities.Pessoas.Autenticacao;

namespace Ecommerce.Domain.Interfaces.Repository;

public interface IUsuarioRepository : IRepositoryBase<Usuario>
{
    Task<Usuario?> ObterUsuarioPorId(int id);

    Task<Usuario?> ObterUsuarioPorEmail(string email);

    Task<Usuario> Inserir(Usuario usuario);
}