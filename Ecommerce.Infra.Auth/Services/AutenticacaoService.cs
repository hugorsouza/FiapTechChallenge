using Ecommerce.Application.Model.Autenticacao;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
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
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPessoaFisicaRepository _pessoaFisicaRepository;
        private readonly IValidator<LoginModel> _validadorLogin;

        public AutenticacaoService(ISenhaHasher hasher, IUsuarioRepository usuarioRepository, IPessoaFisicaRepository pessoaFisicaRepository, IJwtFactory jwtFactory, IValidator<LoginModel> validadorLogin)
        {
            _hasher = hasher;
            _usuarioRepository = usuarioRepository;
            _pessoaFisicaRepository = pessoaFisicaRepository;
            _jwtFactory = jwtFactory;
            _validadorLogin = validadorLogin;
        }

        public async Task<LoginResponse> Login(LoginModel credenciais)
        {
            await _validadorLogin.ValidateAndThrowAsync(credenciais);
            var usuario = await _usuarioRepository.ObterUsuarioPorEmail(credenciais.Email.Trim());
            if(usuario is null || !_hasher.ValidarSenha(credenciais.Senha, usuario.Senha))
                throw RequisicaoInvalidaException.PorMotivo("Credenciais inválidas");

            if (usuario.PessoaFisica is null)
                throw new NotImplementedException("Ainda não temos login por empresa");

            var accessToken = _jwtFactory.GenerateAccessToken(usuario.PessoaFisica);
            var refreshToken = _jwtFactory.GenerateAccessToken(usuario.PessoaFisica);

            return new LoginResponse
            {
                AccessToken = accessToken.Token,
                RefreshToken = refreshToken.Token,
                ExpiraEmUtc = accessToken.DtExpiracaoUtc
            };
        }
    }
}
