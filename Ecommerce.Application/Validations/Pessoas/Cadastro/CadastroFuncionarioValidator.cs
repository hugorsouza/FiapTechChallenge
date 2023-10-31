using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Domain.Interfaces.Repository;
using FluentValidation;

namespace Ecommerce.Application.Validations.Pessoas.Cadastro;

public class CadastroFuncionarioValidator : AbstractValidator<CadastroFuncionarioModel>
{
    public CadastroFuncionarioValidator(IFuncionarioRepository funcionarioRepository, IUsuarioRepository usuarioRepository)
    {
        Include(new CadastroPessoaFisicaValidator(usuarioRepository));

        RuleFor(x => x.Cargo)
            .NotEmpty()
            .Length(6, 255);
        RuleFor(x => x.Cpf)
            .Must(cpf => funcionarioRepository.ObterPorCpf(cpf) is null)
            .WithMessage("Já existe um funcionário com esse CPF");
    }
}