using Ecommerce.Application.Model.Pessoas.Autenticacao;
using Ecommerce.Application.Validations.Pessoas.Autenticacao;

namespace Ecommerce.Application.Tests.CasosTeste.Pessoas.Autenticacao
{
    public class LoginTest
    {
        private readonly LoginModelValidation _validator;

        public LoginTest()
        {
            _validator = new LoginModelValidation();
        }


        [Theory(DisplayName = "Teste de e-mails inválidos")]
        [InlineData("")]
        [InlineData("asdlkasjdklasjd")]
        [InlineData("@")]
        [InlineData("123@")]
        [InlineData("@123")]
        public async Task Dado_EmailInvalido_Deve_RetornarErroVal(string email)
        {
            var login = new LoginModel { Email = email, Senha = Guid.NewGuid().ToString()};

            var resultado = await _validator.TestValidateAsync(login);
            resultado.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Theory(DisplayName = "Teste de senhas inválidas")]
        [InlineData("1")]
        [InlineData("")]
        [InlineData("123@")]
        [InlineData("12345")]
        public async Task Dado_SenhaInvalida_Deve_RetornarErroVal(string senha)
        {
            var login = new LoginModel { Email = "unit.test@hotmail.com", Senha = senha };

            var resultado = await _validator.TestValidateAsync(login);
            resultado.ShouldHaveValidationErrorFor(x => x.Senha);
        }
    }
}
