using Ecommerce.Domain.Entities.Shared;
using Ecommerce.Infra.Auth.Models;

namespace Ecommerce.Infra.Auth.Interfaces
{
    public interface IJwtFactory
    {
        JwtToken GenerateAccessToken(IUsuario usuario);
        JwtToken GenerateRefreshToken(IUsuario usuario, DateTime? validoAte = null);
    }
}
