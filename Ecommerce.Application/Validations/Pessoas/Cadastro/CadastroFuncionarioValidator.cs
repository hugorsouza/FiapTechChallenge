using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Application.Model.Pessoas.Cadastro;
using FluentValidation;

namespace Ecommerce.Application.Validations.Pessoas.Cadastro;

public class CadastroFuncionarioValidator : AbstractValidator<CadastroFuncionarioModel>
{
    public CadastroFuncionarioValidator()
    {
        Include(new CadastroPessoaFisicaValidator());

        RuleFor(x => x.Cargo)
            .NotEmpty()
            .Length(6, 255);
    }
}