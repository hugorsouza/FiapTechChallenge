using Ecommerce.Domain.Entities.Pessoas.Autenticacao;

namespace Ecommerce.Infra.Auth.Models
{
    public class JwtToken
    {
        public JwtToken(int usuarioId, Guid jti, TipoToken tipoToken, string token, DateTime dtCriadoUtc, DateTime dtExpiracaoUtc)
        {
            UsuarioId = usuarioId;
            Jti = jti;
            TipoToken = tipoToken;
            Token = token;
            DtCriadoUtc = dtCriadoUtc;
            DtExpiracaoUtc = dtExpiracaoUtc;
        }

        public int UsuarioId { get; }
        public Guid Jti { get; }
        public TipoToken TipoToken { get; }
        public string Token { get; }
        public DateTime DtCriadoUtc { get; }
        public DateTime DtExpiracaoUtc { get; }
    }
}
