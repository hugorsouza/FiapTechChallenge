using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Application.Model.Pessoas.Cadastro;
using FluentValidation;

namespace Ecommerce.Application.Validations.Pessoas.Cadastro
{
    public class AlterarClienteValidator : AbstractValidator<AlterarClienteModel>
    {
        public AlterarClienteValidator()
        {
            Include(new AlterarPessoaFisicaValidator());
        }
    }
}