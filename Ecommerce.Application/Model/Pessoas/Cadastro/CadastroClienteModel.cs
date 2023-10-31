namespace Ecommerce.Application.Model.Pessoas.Cadastro;

public record CadastroClienteModel : CadastroPessoaModelBase
{
    public bool RecebeNewsletterEmail { get; set; }
}