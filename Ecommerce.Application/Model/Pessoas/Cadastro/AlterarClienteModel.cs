namespace Ecommerce.Application.Model.Pessoas.Cadastro;

public record AlterarClienteModel : AlterarPessoaModelBase
{
    public bool RecebeNewsletterEmail { get; set; }
}