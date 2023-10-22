using Ecommerce.Application.Model.Pessoas.Cadastro;
using FluentValidation;

namespace Ecommerce.Application.Validations.Pessoas.Cadastro;

public class CadastroClienteValidator : AbstractValidator<CadastroClienteModel>
{
    public CadastroClienteValidator()
    {
        Include(new CadastroPessoaFisicaValidator());
    }
}