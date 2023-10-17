using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Autenticacao;
using Ecommerce.Domain.Entities.Shared;
using Ecommerce.Infra.Auth.Constants;

namespace Ecommerce.Infra.Auth.Extensions
{
    public static class UsuarioExtensions
    {
        public static List<Claim> ObterClaims(this IUsuario pessoa)
        {
            var perfil = PerfilUsuarioHelper.ObterClaimPerfil(pessoa.Usuario.Perfil);
            var claims = new List<Claim> {
                new(JwtRegisteredClaimNames.Sub, pessoa.Usuario.Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, pessoa.Usuario.EmailNormalizado),
                new(JwtRegisteredClaimNames.Email, pessoa.Usuario.EmailNormalizado),
                new(JwtRegisteredClaimNames.Name, pessoa.Usuario.NomeExibicao),
                new(ClaimsIdentity.DefaultNameClaimType, pessoa.Usuario.NomeExibicao),
                new(ClaimsIdentity.DefaultRoleClaimType, perfil)
            };

            if (pessoa is PessoaFisica pessoaFisica)
            {
                claims.Add(
                    new(JwtRegisteredClaimNames.GivenName, pessoaFisica.Nome));
                claims.Add(
                    new(JwtRegisteredClaimNames.GivenName, pessoaFisica.Sobrenome));
                claims.Add(
                    new(JwtRegisteredClaimNames.Birthdate, pessoaFisica.DataNascimento.ToString("s")));
                claims.Add(new(CustomClaims.TipoPessoa, nameof(PessoaFisica)));
            }

            return claims;
        }
    }
}
