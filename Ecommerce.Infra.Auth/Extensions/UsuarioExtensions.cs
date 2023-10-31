using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Fisica;
using Ecommerce.Infra.Auth.Constants;

namespace Ecommerce.Infra.Auth.Extensions
{
    public static class UsuarioExtensions
    {
        public static Claim[] ObterClaims(this Usuario usuario)
        {
            var perfil = PerfilUsuarioExtensions.ObterClaimPerfil(usuario.Perfil);
            var claims = new List<Claim> {
                new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, usuario.EmailNormalizado),
                new(JwtRegisteredClaimNames.Email, usuario.EmailNormalizado),
                new(JwtRegisteredClaimNames.Name, usuario.NomeExibicao),
                new(ClaimsIdentity.DefaultNameClaimType, usuario.NomeExibicao),
                new(ClaimsIdentity.DefaultRoleClaimType, perfil)
            };

            switch (usuario.Perfil)
            {
                case PerfilUsuario.Cliente:
                    claims.AddRange(ObterClaimsPessoa(usuario.Cliente));
                    break;
                case PerfilUsuario.Funcionario:
                    claims.AddRange(ObterClaimsPessoa(usuario.Funcionario));
                    claims.Add(new(CustomClaims.FlagAdmin, usuario.Funcionario.Administrador.ToString().ToLower()));
                    break;
                default:
                    throw new InvalidEnumArgumentException($"O perfil {usuario.Perfil} não foi implementado");
            }

            return claims.ToArray();
        }
        
        private static Claim[] ObterClaimsPessoa(PessoaFisica pessoaFisica)
        {
            var claims = new Claim[]
            {
                new(JwtRegisteredClaimNames.GivenName, pessoaFisica.Nome),
                new(JwtRegisteredClaimNames.FamilyName, pessoaFisica.Sobrenome),
                new(JwtRegisteredClaimNames.Birthdate, pessoaFisica.DataNascimento.ToString("s")),
                new(CustomClaims.TipoPessoa, nameof(PessoaFisica))
            };
            return claims;
        }
    }
}
