namespace Ecommerce.Domain.Entities.Pessoas.Autenticacao;

public enum PerfilUsuario
{
    Cliente,
    Funcionario,
    EmpresaTerceira
}

public static class PerfilUsuarioExtensions
{
    public const string Cliente = "C";
    public const string Funcionario = "F";
    public const string EmpresaTerceira = "T";

    public static string ObterClaimPerfil(PerfilUsuario perfil)
    {
        return perfil switch
        {
            PerfilUsuario.Cliente => Cliente,
            PerfilUsuario.Funcionario => Funcionario,
            PerfilUsuario.EmpresaTerceira => EmpresaTerceira,
            _ => throw new NotImplementedException($"Perfil [{perfil}] não implementado")
        };
    }
}