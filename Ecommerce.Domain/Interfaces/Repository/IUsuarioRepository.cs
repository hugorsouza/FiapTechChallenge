using Ecommerce.Domain.Entities.Pessoas.Autenticacao;

namespace Ecommerce.Domain.Interfaces.Repository;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Usuario ObterUsuarioPorEmail(string email);

    int CadastrarObterId(Usuario usuario);
}