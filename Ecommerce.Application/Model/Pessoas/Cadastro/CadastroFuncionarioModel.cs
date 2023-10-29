namespace Ecommerce.Application.Model.Pessoas.Cadastro;

public record CadastroFuncionarioModel : CadastroPessoaModelBase
{
    public string Cargo { get; set; }
    public bool Administrador { get; set; }
}