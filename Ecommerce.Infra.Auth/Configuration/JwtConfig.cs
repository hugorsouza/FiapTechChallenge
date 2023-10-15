using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ecommerce.Infra.Auth.Configuration
{
    public class JwtConfig
    {
        public static readonly string AppSettingsKey = "JwtConfig";
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenDuracaoMin { get; set; }

        public int RefreshTokenDuracaoMin { get; set; }
        public string SecretKey { get; set; }

        public JwtConfig(string audience, string issuer, int accessTokenExpireMin, int refreshMinExpire, string secretKey)
        {
            Audience = audience;
            Issuer = issuer;
            AccessTokenDuracaoMin = accessTokenExpireMin;
            RefreshTokenDuracaoMin = refreshMinExpire;
            SecretKey = secretKey;
        }

        public JwtConfig() { }

        public SecurityKey GetSecurityKey()
        {
            if (string.IsNullOrEmpty(SecretKey))
                throw new InvalidOperationException($"{nameof(SecretKey)} não pode estar nulo");

            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        }

        public SigningCredentials GetSigningCredentials()
        {
            var securityKey = GetSecurityKey();
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        }
    }
}
