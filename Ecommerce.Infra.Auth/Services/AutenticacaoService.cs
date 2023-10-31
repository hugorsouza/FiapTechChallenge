using Ecommerce.Application.Model.Pessoas.Autenticacao;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Infra.Auth.Constants;
using Ecommerce.Infra.Auth.Interfaces;
using FluentValidation;

namespace Ecommerce.Infra.Auth.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly IJwtHelper _jwtHelper;
        private readonly ISenhaHasher _hasher;
        private readonly IUsuarioManager _usuarioManager;
        private readonly IValidator<LoginModel> _validadorLogin;
        private readonly IValidator<RefreshLoginModel> _validadorRefresh;

        public AutenticacaoService(ISenhaHasher hasher, IUsuarioManager usuarioManager, IJwtHelper jwtHelper, IValidator<LoginModel> validadorLogin, IValidator<RefreshLoginModel> validadorRefresh)
        {
            _hasher = hasher;
            _usuarioManager = usuarioManager;
            _jwtHelper = jwtHelper;
            _validadorLogin = validadorLogin;
            _validadorRefresh = validadorRefresh;
        }

        public async Task<LoginWithRefreshResponse> Login(LoginModel credenciais)
        {
            await _validadorLogin.ValidateAndThrowAsync(credenciais);
            var usuario = _usuarioManager.ObterUsuarioPorEmail(credenciais.Email.Trim());
            if(UsuarioInvalido(usuario) || !_hasher.ValidarSenha(credenciais.Senha, usuario.Senha))
                throw RequisicaoInvalidaException.PorMotivo("Credenciais inválidas");
            
            return GerarTokens(usuario);
        }

        public async Task<LoginResponse> RefreshLogin(RefreshLoginModel credenciais)
        {
            await _validadorRefresh.ValidateAndThrowAsync(credenciais);
            var usuario = _usuarioManager.ObterUsuarioPorEmail(credenciais.Email.Trim());
            if (UsuarioInvalido(usuario) || RefreshTokenInvalido(credenciais.RefreshToken, usuario.Id))
                throw RequisicaoInvalidaException.PorMotivo("Credenciais inválidas");

            return GerarToken(usuario);
        }

        private LoginResponse GerarToken(Usuario usuario)
        {
            var accessToken = _jwtHelper.GenerateAccessToken(usuario);
            return new LoginResponse
            {
                AccessToken = accessToken.Token,
                ExpiraEmUtc = accessToken.DtExpiracaoUtc
            };
        }

        private LoginWithRefreshResponse GerarTokens(Usuario usuario)
        {
            var accessToken = _jwtHelper.GenerateAccessToken(usuario);
            var refreshToken = _jwtHelper.GenerateRefreshToken(usuario);

            return new LoginWithRefreshResponse
            {
                AccessToken = accessToken.Token,
                RefreshToken = refreshToken.Token,
                ExpiraEmUtc = accessToken.DtExpiracaoUtc
            };
        }

        private bool RefreshTokenInvalido(string token, int usuarioIdProprietario)
        {
            if(!_jwtHelper.TentarLerToken(token, out var jwt))
                return true;
            if (!int.TryParse(jwt.Subject, out var usuarioIdToken) || usuarioIdToken != usuarioIdProprietario)
                return true;
            return !jwt.TryGetClaim(CustomClaims.TipoToken, out var claimTipo)
                || !int.TryParse(claimTipo.Value, out var tipoToken)
                || tipoToken != (int)TipoToken.RefreshToken;
        }

        private static bool UsuarioInvalido(Usuario usuario)
        {
            return usuario is null || !usuario.Ativo;
        }
    }
}
