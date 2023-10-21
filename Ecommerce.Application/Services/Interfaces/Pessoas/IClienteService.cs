using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Domain.Entities.Pessoas.Fisica;

namespace Ecommerce.Application.Services.Interfaces.Pessoas;

public interface IClienteService
{
    Task<ClienteViewModel> Cadastrar(CadastroClienteModel model);
}