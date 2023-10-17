using Ecommerce.Domain.Entities.Autenticacao;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Dados.Context;
using Ecommerce.Infra.Dados.Repositories.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infra.Dapper.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
        }

        private IQueryable<Usuario> QueryComDadosUsuario => DbSet.Include(x => x.PessoaFisica);

        public async Task<Usuario?> ObterUsuarioPorId(int id)
        {
            return QueryComDadosUsuario.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Usuario?> ObterUsuarioPorEmail(string email)
        {
            email = email.ToUpperInvariant().Trim();
            return QueryComDadosUsuario.FirstOrDefault(x => x.EmailNormalizado == email.ToUpperInvariant().Trim());
        }

        public async Task<Usuario> Inserir(Usuario usuario)
        {
            DbSet.Add(usuario);
            return usuario;
        }
    }
}
