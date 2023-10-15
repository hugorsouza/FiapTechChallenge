namespace Ecommerce.Application.Services.Interfaces.Autenticacao
{
    public interface ISenhaHasher
    {
        bool ValidarSenha(string senhaTexto, string hash);
        string Hash(string senha);
    }
}
