namespace Ecommerce.Application.Model.Pessoas.Cadastro;

public record AlterarFuncionarioModel : AlterarPessoaModelBase
{
    public string Cargo { get; set; }
    public bool Administrador { get; set; }
    public bool Ativo { get; set; }
}