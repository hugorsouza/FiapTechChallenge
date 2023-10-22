using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Application.Validations.Propriedades;
using FluentValidation;

namespace Ecommerce.Application.Validations.Pessoas.Cadastro;

public class CadastroUsuarioValidator: AbstractValidator<CadastroUsuarioModelBase>
{
    public CadastroUsuarioValidator()
    {
        RuleFor(p => p.Email)
            .NotNull()
            .EmailAddress();
        
        RuleFor(p => p.Senha)
            .SetValidator(new SenhaValidator<CadastroUsuarioModelBase, string>());
    }
}