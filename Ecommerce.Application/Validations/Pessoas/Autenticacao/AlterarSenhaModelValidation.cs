using Ecommerce.Application.Model.Pessoas.Autenticacao;
using Ecommerce.Application.Validations.Propriedades;
using FluentValidation;

namespace Ecommerce.Application.Validations.Pessoas.Autenticacao;

public class AlterarSenhaModelValidation : AbstractValidator<AlterarSenhaModel>
{
    public AlterarSenhaModelValidation()
    {
        RuleFor(x => x.Senha)
            .SetValidator(new SenhaValidator<AlterarSenhaModel, string>());
    }
}