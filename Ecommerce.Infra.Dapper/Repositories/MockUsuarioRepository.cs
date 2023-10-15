using System.Collections.Concurrent;
using Ecommerce.Domain.Entity.Autenticacao;
using Ecommerce.Domain.Interfaces.Repository;

namespace Ecommerce.Infra.Dapper.Repositories
{
    public class MockUsuarioRepository : IUsuarioRepository
    {
        private static readonly ConcurrentBag<Usuario> _usuarios = new(new []
        {
            UsuarioMock("operador@hotmail.com", PerfilUsuario.Operador, "$2a$12$waDJlmbK5LBJTdvIIQyZCOOu0D4Y0bq5T1UHzaA5qJ/BazhLf.P9G"),//123456
            UsuarioMock("cliente@hotmail.com", PerfilUsuario.Cliente, "$2a$12$waDJlmbK5LBJTdvIIQyZCOOu0D4Y0bq5T1UHzaA5qJ/BazhLf.P9G"),//123456
        });

        public async Task<Usuario?> ObterUsuarioPorId(int id)
        {
            return _usuarios.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Usuario?> ObterUsuarioPorEmail(string email)
        {
            return _usuarios.FirstOrDefault(x => x.EmailNormalizado == email.ToUpperInvariant().Trim());
        }

        public async Task<Usuario> Inserir(Usuario usuario)
        {
            usuario.Id = MockId();
            _usuarios.Add(usuario);
            return usuario;
        }

        public async Task<Usuario?> Update(Usuario usuario)
        {
            var oldUsuario = await ObterUsuarioPorId(usuario.Id);
            if (oldUsuario is null)
                return null;

            oldUsuario.NomeExibicao = usuario.NomeExibicao;
            oldUsuario.DataAlteracao = usuario.DataAlteracao;
            oldUsuario.Senha = usuario.Senha;
            return usuario;
        }

        private static Usuario UsuarioMock(string email, PerfilUsuario perfil, string senhaBcrypt)
        {
            return new Usuario
            {
                Id = MockId(),
                Email = email,
                EmailNormalizado = email.ToUpperInvariant(),
                DataCadastro = DateTime.Now.AddDays(-2),
                DataAlteracao = DateTime.Now,
                NomeExibicao = $"{perfil} de teste",
                Perfil = perfil,
                Senha = senhaBcrypt
            };
        }

        private static int MockId() => !_usuarios?.Any() ?? true ? 1 : _usuarios.Max(x => x.Id) + 1;
    }
}
