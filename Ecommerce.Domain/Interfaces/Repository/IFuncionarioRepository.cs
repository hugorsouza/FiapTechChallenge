using Ecommerce.Domain.Entities.Pessoas.Fisica;

namespace Ecommerce.Domain.Interfaces.Repository;

public interface IFuncionarioRepository : IRepository<Funcionario>
{
    Funcionario ObterPorCpf(string cpf);
}