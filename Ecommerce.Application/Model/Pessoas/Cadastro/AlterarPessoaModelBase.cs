namespace Ecommerce.Application.Model.Pessoas.Cadastro;

public abstract record AlterarPessoaModelBase
{
    public string Nome { get; init; }
    public string Sobrenome { get; init; }
    public DateTime DataNascimento { get; init; }

    public string ObterNomeExibicao()
    {
        return $"{Nome} {Sobrenome}".Trim();
    }
}