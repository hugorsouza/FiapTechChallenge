namespace Ecommerce.Application.Model.Pessoas;

public record CadastroFuncionarioModel : CadastroPessoaModelBase
{
    public string Cargo { get; set; }
}