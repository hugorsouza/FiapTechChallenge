using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Infra.Auth.Models;

namespace Ecommerce.Infra.Auth.Interfaces
{
    public interface IJwtFactory
    {
        JwtToken GenerateAccessToken(Usuario usuario);
        JwtToken GenerateRefreshToken(Usuario usuario, DateTime? validoAte = null);
    }
}
