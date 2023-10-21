using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Pessoas;
using Ecommerce.Domain.Entities.Pessoas.Fisica;

namespace Ecommerce.Domain.Interfaces.Repository
{
    public interface IClienteRepository : IRepositoryBase<Cliente>
    {
        Task<Cliente> ObterPessoaPorId(int id);
        Task<Cliente> Inserir(Cliente cliente);
    }
}
