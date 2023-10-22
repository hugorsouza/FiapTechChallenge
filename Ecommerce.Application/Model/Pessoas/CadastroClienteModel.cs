namespace Ecommerce.Application.Model.Pessoas;

public record CadastroClienteModel : CadastroPessoaModelBase
{
    public bool RecebeNewsletterEmail { get; set; }
}