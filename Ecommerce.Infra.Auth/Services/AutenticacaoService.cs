using Ecommerce.Application.Model.Pessoas.Autenticacao;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Auth.Interfaces;
using FluentValidation;

namespace Ecommerce.Infra.Auth.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly ISenhaHasher _hasher;
        private readonly IUsuarioManager _usuarioManager;
        private readonly IValidator<LoginModel> _validadorLogin;

        public AutenticacaoService(ISenhaHasher hasher, IUsuarioManager usuarioManager, IJwtFactory jwtFactory, IValidator<LoginModel> validadorLogin)
        {
            _hasher = hasher;
            _usuarioManager = usuarioManager;
            _jwtFactory = jwtFactory;
            _validadorLogin = validadorLogin;
        }

        public async Task<LoginResponse> Login(LoginModel credenciais)
        {
            await _validadorLogin.ValidateAndThrowAsync(credenciais);
            var usuario = _usuarioManager.ObterUsuarioPorEmail(credenciais.Email.Trim());
            if(usuario is null || !_hasher.ValidarSenha(credenciais.Senha, usuario.Senha))
                throw RequisicaoInvalidaException.PorMotivo("Credenciais inválidas");
            
            return GerarTokens(usuario);
        }

        private LoginResponse GerarTokens(Usuario usuario)
        {
            var accessToken = _jwtFactory.GenerateAccessToken(usuario);
            var refreshToken = _jwtFactory.GenerateRefreshToken(usuario);

            return new LoginResponse
            {
                AccessToken = accessToken.Token,
                RefreshToken = refreshToken.Token,
                ExpiraEmUtc = accessToken.DtExpiracaoUtc
            };
        }
    }
}
