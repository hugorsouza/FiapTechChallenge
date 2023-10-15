namespace Ecommerce.Domain.Entity.Autenticacao;

public enum PerfilUsuario
{
    Cliente,
    Operador,
    Terceiros
}

public static class PerfilUsuarioHelper
{
    public const string Cliente = "C";
    public const string Operador = "O";
    public const string Terceiros = "T";

    public static string ObterClaimPerfil(PerfilUsuario perfil)
    {
        return perfil switch
        {
            PerfilUsuario.Cliente => Cliente,
            PerfilUsuario.Operador => Operador,
            PerfilUsuario.Terceiros => Terceiros,
            _ => throw new NotImplementedException($"Perfil [{perfil}] não implementado")
        };
    }
}