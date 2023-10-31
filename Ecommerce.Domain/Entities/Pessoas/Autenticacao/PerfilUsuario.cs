namespace Ecommerce.Domain.Entities.Pessoas.Autenticacao;

public enum PerfilUsuario
{
    Cliente,
    Funcionario
}

public static class PerfilUsuarioExtensions
{
    public const string Cliente = "C";
    public const string Funcionario = "F";

    public static string ObterClaimPerfil(PerfilUsuario perfil)
    {
        return perfil switch
        {
            PerfilUsuario.Cliente => Cliente,
            PerfilUsuario.Funcionario => Funcionario,
            _ => throw new NotImplementedException($"Perfil [{perfil}] não implementado")
        };
    }
}