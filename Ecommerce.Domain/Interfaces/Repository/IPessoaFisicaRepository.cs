using Ecommerce.Domain.Entity;

namespace Ecommerce.Domain.Interfaces.Repository
{
    public interface IPessoaFisicaRepository
    {
        Task<PessoaFisica?> ObterPessoaPorId(int id);
        Task<PessoaFisica> Inserir(PessoaFisica pessoa);
        Task<PessoaFisica?> Update(PessoaFisica pessoa);
        Task<PessoaFisica> ObterPessoaPorUsuario(int usuarioId);
    }
}
