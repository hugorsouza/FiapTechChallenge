using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Application.Model.Pessoas.Cadastro;
using FluentValidation;

namespace Ecommerce.Application.Validations.Pessoas.Cadastro
{
    public class AlterarPessoaFisicaValidator : AbstractValidator<AlterarPessoaModelBase>
    {
        public AlterarPessoaFisicaValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty();

            RuleFor(p => p.Sobrenome)
                .NotEmpty();

            RuleFor(p => p.DataNascimento)
                .NotEmpty()
                .WithMessage("A data de nascimento é obrigatória.")
                .LessThan(DateTime.UtcNow)
                .WithMessage("A data de nascimento não pode ser no futuro.");
        }
    }
}