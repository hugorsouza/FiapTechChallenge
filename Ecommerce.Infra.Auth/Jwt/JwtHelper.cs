using Ecommerce.Infra.Auth.Configuration;
using Ecommerce.Infra.Auth.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Infra.Auth.Constants;
using Ecommerce.Infra.Auth.Interfaces;
using Ecommerce.Infra.Auth.Extensions;

namespace Ecommerce.Infra.Auth.Jwt
{
    public class JwtHelper : IJwtHelper
    {
        private readonly JwtConfig _jwtConfig;
        public JwtHelper(IOptions<JwtConfig> jwtConfig)
        {
            ArgumentNullException.ThrowIfNull(jwtConfig);
            _jwtConfig = jwtConfig.Value;
        }

        public JwtToken GenerateAccessToken(Usuario usuario)
        {
            var criadoEm = DateTime.UtcNow;
            var expiraEm = criadoEm.AddTicks(TicksAteExpiracaoToken());
            var token = GenerateToken(usuario, criadoEm, expiraEm, TipoToken.AccessToken);
            return token;
        }

        public JwtToken GenerateRefreshToken(Usuario usuario, DateTime? validoAte = null)
        {
            var criadoEm = DateTime.UtcNow;
            var expiraEm = validoAte ?? criadoEm.AddTicks(TicksAteExpiracaoRefreshToken());
            var token = GenerateToken(usuario, criadoEm, expiraEm, TipoToken.RefreshToken);
            return token;
        }

        private JwtToken GenerateToken(Usuario user, DateTime creationDate, DateTime expirationDate, TipoToken tipoToken)
        {
            var signingCredentials = ObterSigningCredentials();
            var userClaims = user.ObterClaims().ToList();

            AdicionarClaimsObrigatorias(userClaims, out var jti);

            userClaims.Add(new Claim(CustomClaims.TipoToken, ((int)tipoToken).ToString()));
            var securityToken = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(userClaims.ToArray()),
                Issuer = ObterIssuer(),
                Audience = ObterAudience(),
                IssuedAt = creationDate,
                Expires = expirationDate,
                SigningCredentials = signingCredentials
            };

            var tokenString = new JsonWebTokenHandler().CreateToken(securityToken);
            return new JwtToken(user.Id, jti, tipoToken, tokenString, creationDate, expirationDate);
        }

        public bool TentarLerToken(string tokenString, out JsonWebToken token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tokenString))
                {
                    token = null;
                    return false;
                }

                token = LerToken(tokenString);
                return token is not null;
            }
            catch
            {
                token = null;
                return false;
            }
        }

        public JsonWebToken LerToken(string tokenString)
        {
            var token = new JsonWebTokenHandler().ReadJsonWebToken(tokenString);
            return token;
        }

        public string ObterIssuer()
        {
            if (string.IsNullOrWhiteSpace(_jwtConfig.Issuer))
                throw new InvalidOperationException($"{nameof(_jwtConfig.Issuer)} não pode ser nulo ou vazio");

            return _jwtConfig.Issuer;
        }

        public string ObterAudience()
        {
            if (string.IsNullOrWhiteSpace(_jwtConfig.Audience))
                throw new InvalidOperationException($"{nameof(_jwtConfig.Audience)} não pode ser nulo ou vazio");

            return _jwtConfig.Audience;
        }

        public SecurityKey ObterSecurityKey() => _jwtConfig.GetSecurityKey();

        public SigningCredentials ObterSigningCredentials() => _jwtConfig.GetSigningCredentials();

        private long TicksAteExpiracaoToken()
        {
            if (_jwtConfig.AccessTokenDuracaoMin <= 0)
                throw new InvalidOperationException($"{nameof(_jwtConfig.AccessTokenDuracaoMin)} deve ser maior que 0");

            return TimeSpan.FromMinutes(_jwtConfig.AccessTokenDuracaoMin).Ticks;
        }

        private long TicksAteExpiracaoRefreshToken()
        {
            if (_jwtConfig.RefreshTokenDuracaoMin <= 0)
                throw new InvalidOperationException($"{nameof(_jwtConfig.RefreshTokenDuracaoMin)} deve ser maior que 0");

            return TimeSpan.FromMinutes(_jwtConfig.RefreshTokenDuracaoMin).Ticks;
        }

        private void AdicionarClaimsObrigatorias(List<Claim> claims, out Guid jti)
        {
            jti = Guid.NewGuid();
            AdicionarClaimsSeNaoExistir(new(JwtRegisteredClaimNames.Jti, jti.ToString()), claims);
        }

        private static void AdicionarClaimsSeNaoExistir(Claim claim, List<Claim> claims)
        {
            if (claims.Any(c => c.Type == claim.Type))
                return;
            claims.Add(claim);
        }
    }
}
