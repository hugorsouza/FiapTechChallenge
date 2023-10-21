using Ecommerce.Application.Model.Autenticacao;
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
                .NotEmpty()
                .Length(6, 255);
        }
    }
}
