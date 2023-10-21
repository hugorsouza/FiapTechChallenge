using Ecommerce.Application.Model.Pessoas;
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
            .NotNull()
            .Length(6, 255);
    }
}