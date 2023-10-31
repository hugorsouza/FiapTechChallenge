using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Pessoas;
using Ecommerce.Domain.Entities.Pessoas.Fisica;

namespace Ecommerce.Domain.Interfaces.Repository
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Cliente ObterPorCpf(string cpf);
    }
}
