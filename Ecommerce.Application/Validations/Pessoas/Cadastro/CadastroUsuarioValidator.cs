using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Application.Validations.Propriedades;
using Ecommerce.Domain.Entities.Shared;
using Ecommerce.Domain.Interfaces.Repository;
using FluentValidation;

namespace Ecommerce.Application.Validations.Pessoas.Cadastro;

public class CadastroUsuarioValidator: AbstractValidator<CadastroUsuarioModelBase>
{
    public CadastroUsuarioValidator(IUsuarioRepository usuarioRepository)
    {
        RuleFor(p => p.Email)
            .NotNull()
            .EmailAddress()
            .Must(email => usuarioRepository.ObterUsuarioPorEmail(email?.Trim().ToUpperInvariant()) is null)
            .WithMessage("Já existe um usuário cadastrado com esse e-mail");
        
        RuleFor(p => p.Senha)
            .SetValidator(new SenhaValidator<CadastroUsuarioModelBase, string>());
    }
}