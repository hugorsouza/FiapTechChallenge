using Ecommerce.Application.Configuration;
using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Domain.Interfaces.Repository;
using FluentValidation;

namespace Ecommerce.Application.Validations.Pessoas.Cadastro
{
    public class CadastroPessoaFisicaValidator : AbstractValidator<CadastroPessoaModelBase>
    {
        public CadastroPessoaFisicaValidator(IUsuarioRepository usuarioRepository)
        {
            Include(new CadastroUsuarioValidator(usuarioRepository));
            
            RuleFor(p => p.Nome)
                .NotEmpty();

            RuleFor(p => p.Sobrenome)
                .NotEmpty();

            RuleFor(p => p.Cpf)
                .NotEmpty()
                .Must(ValidarDocumento.IsCpf);

            RuleFor(p => p.DataNascimento)
                .NotEmpty()
                .WithMessage("A data de nascimento é obrigatória.")
                .LessThan(DateTime.UtcNow)
                .WithMessage("A data de nascimento não pode ser no futuro.");
        }
    }
}