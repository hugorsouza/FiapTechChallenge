using Ecommerce.Domain.Entities.Pessoas.Fisica;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Dados.Context;
using Ecommerce.Infra.Dados.Repositories.Shared;

namespace Ecommerce.Infra.Dados.Repositories.Pessoas;

public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
{
    public ClienteRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Cliente?> ObterPessoaPorId(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<Cliente> Inserir(Cliente cliente)
    {
        DbSet.Add(cliente);
        await Context.SaveChangesAsync();
        return cliente;
    }
}