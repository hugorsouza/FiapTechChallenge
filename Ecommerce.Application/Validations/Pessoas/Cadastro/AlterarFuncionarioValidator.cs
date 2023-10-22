using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Application.Model.Pessoas.Cadastro;
using FluentValidation;

namespace Ecommerce.Application.Validations.Pessoas.Cadastro
{
    public class AlterarFuncionarioValidator : AbstractValidator<AlterarFuncionarioModel>
    {
        public AlterarFuncionarioValidator()
        {
            Include(new AlterarPessoaFisicaValidator());
            RuleFor(x => x.Cargo)
                .NotEmpty();
        }
    }
}