using System.Collections.Concurrent;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Autenticacao;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Dados.Context;
using Ecommerce.Infra.Dados.Repositories.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infra.Dapper.Repositories
{
    public class PessoaFisicaRepository : RepositoryBase<PessoaFisica>, IPessoaFisicaRepository
    {
        public PessoaFisicaRepository(ApplicationDbContext context) : base(context)
        {
        }

        private IQueryable<PessoaFisica> QueryComUsuario => DbSet.Include(x => x.Usuario);

        public async Task<PessoaFisica?> ObterPessoaPorId(int id)
        {
            return QueryComUsuario.FirstOrDefault(x => x.Id == id);
        }

        public async Task<PessoaFisica?> ObterPessoaPorUsuario(int usuarioId)
        {
            return QueryComUsuario.FirstOrDefault(x => x.Usuario.Id == usuarioId);
        }

        public async Task<PessoaFisica> Inserir(PessoaFisica pessoa)
        {
            DbSet.Add(pessoa);
            return pessoa;
        }
    }
}
