using Ecommerce.Application.Model.Pessoas.Autenticacao;
using FluentValidation;

namespace Ecommerce.Application.Validations.Pessoas.Autenticacao
{
    public class RefreshLoginModelValidation : AbstractValidator<RefreshLoginModel>
    {
        public RefreshLoginModelValidation()
        {
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.RefreshToken)
                .NotEmpty();
        }
    }
}
