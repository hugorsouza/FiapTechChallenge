using Ecommerce.Application.Services.Interfaces.Autenticacao;

namespace Ecommerce.Infra.Auth.Services
{
    public class BCryptSenhaHasher : ISenhaHasher
    {
        public bool ValidarSenha(string senhaTexto, string hash)
        {
            if (string.IsNullOrWhiteSpace(senhaTexto))
                return false;

            return BCrypt.Net.BCrypt.Verify(text: senhaTexto, hash: hash);
        }

        public string Hash(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }
    }
}
