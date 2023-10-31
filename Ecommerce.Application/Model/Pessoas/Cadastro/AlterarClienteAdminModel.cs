namespace Ecommerce.Application.Model.Pessoas.Cadastro;

public record AlterarClienteAdminModel : AlterarClienteModel
{
    public bool Ativo { get; set; }
}