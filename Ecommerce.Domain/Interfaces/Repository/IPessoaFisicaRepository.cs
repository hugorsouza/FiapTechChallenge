using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Autenticacao;

namespace Ecommerce.Domain.Interfaces.Repository
{
    public interface IPessoaFisicaRepository : IRepositoryBase<PessoaFisica>
    {
        Task<PessoaFisica?> ObterPessoaPorId(int id);
        Task<PessoaFisica> Inserir(PessoaFisica pessoa);
        Task<PessoaFisica> ObterPessoaPorUsuario(int usuarioId);
    }
}
