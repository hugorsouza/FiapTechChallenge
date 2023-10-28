using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Infra.Auth.Models;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Ecommerce.Infra.Auth.Interfaces
{
    public interface IJwtHelper
    {
        JwtToken GenerateAccessToken(Usuario usuario);
        JwtToken GenerateRefreshToken(Usuario usuario, DateTime? validoAte = null);
        bool TentarLerToken(string tokenString, out JsonWebToken token);
    }
}
