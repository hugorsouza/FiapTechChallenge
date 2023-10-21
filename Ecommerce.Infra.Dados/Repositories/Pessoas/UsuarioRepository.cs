using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Dados.Context;
using Ecommerce.Infra.Dados.Repositories.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infra.Dados.Repositories.Pessoas
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
        }

        private IQueryable<Usuario> QueryComDadosUsuario => DbSet
            .Include(x => x.Cliente)
            .Include(x => x.Funcionario)
            .Include(x => x.Empresa);

        public async Task<Usuario?> ObterUsuarioPorId(int id)
        {
            return await QueryComDadosUsuario.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Usuario?> ObterUsuarioPorEmail(string email)
        {
            email = email.ToUpperInvariant().Trim();
            return await QueryComDadosUsuario.FirstOrDefaultAsync(x => x.EmailNormalizado == email.ToUpperInvariant().Trim());
        }

        public async Task<Usuario> Inserir(Usuario usuario)
        {
            DbSet.Add(usuario);
            return usuario;
        }
    }
}
