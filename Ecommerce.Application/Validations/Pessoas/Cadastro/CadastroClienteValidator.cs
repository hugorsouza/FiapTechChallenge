using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Domain.Interfaces.Repository;
using FluentValidation;

namespace Ecommerce.Application.Validations.Pessoas.Cadastro;

public class CadastroClienteValidator : AbstractValidator<CadastroClienteModel>
{
    public CadastroClienteValidator(IClienteRepository clienteRepository, IUsuarioRepository usuarioRepository)
    {
        Include(new CadastroPessoaFisicaValidator(usuarioRepository));
        RuleFor(x => x.Cpf)
            .Must(cpf => clienteRepository.ObterPorCpf(cpf) is null)
            .WithMessage("Já existe um cliente com esse CPF");
    }
}