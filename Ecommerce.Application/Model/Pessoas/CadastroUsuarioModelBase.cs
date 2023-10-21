namespace Ecommerce.Application.Model.Pessoas;

public abstract record CadastroUsuarioModelBase
{
    public string Email { get; set; }
    public string Senha { get; set; }
    public abstract string ObterNomeExibicao();
}