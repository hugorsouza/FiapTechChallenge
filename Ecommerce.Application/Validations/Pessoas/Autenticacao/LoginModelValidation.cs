using Ecommerce.Application.Model.Pessoas.Autenticacao;
using Ecommerce.Application.Validations.Propriedades;
using FluentValidation;

namespace Ecommerce.Application.Validations.Pessoas.Autenticacao
{
    public class LoginModelValidation : AbstractValidator<LoginModel>
    {
        public LoginModelValidation()
        {
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Senha)
                .SetValidator(new SenhaValidator<LoginModel, string>());
        }
    }
}
