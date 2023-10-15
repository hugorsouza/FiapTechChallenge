using System.Collections.Concurrent;
using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Interfaces.Repository;

namespace Ecommerce.Infra.Dapper.Repositories
{
    public class MockPessoaFisicaRepository : IPessoaFisicaRepository
    {
        private static readonly ConcurrentBag<PessoaFisica> _pessoas = new(new[]
        {
            PessoaMock("operador@hotmail.com", "Operador", "Sistema", 1),
            PessoaMock("cliente@hotmail.com", "Cliente", "do Site", 2),//123456
        });
        
        public async Task<PessoaFisica?> ObterPessoaPorId(int id)
        {
            return _pessoas.FirstOrDefault(x => x.Id == id);
        }

        public async Task<PessoaFisica?> ObterPessoaPorUsuario(int usuarioId)
        {
            return _pessoas.FirstOrDefault(x => x.UsuarioId == usuarioId);
        }

        public async Task<PessoaFisica> Inserir(PessoaFisica pessoa)
        {
            pessoa.Id = MockId();
            _pessoas.Add(pessoa);
            return pessoa;
        }

        public async Task<PessoaFisica?> Update(PessoaFisica pessoa)
        {
            var oldPessoa = await ObterPessoaPorId(pessoa.Id);
            if (oldPessoa is null)
                return null;

            oldPessoa.Nome = pessoa.Nome;
            oldPessoa.Sobrenome = pessoa.Sobrenome;
            oldPessoa.DataNascimento = pessoa.DataNascimento;
            oldPessoa.DataAlteracao = pessoa.DataAlteracao;
            oldPessoa.Ativo = pessoa.Ativo;
            return pessoa;
        }

        private static PessoaFisica PessoaMock(string cpf, string nome, string sobrenome, int usuarioId)
        {
            return new PessoaFisica
            {
                Id = MockId(),
                UsuarioId = usuarioId,
                Cpf = cpf,
                Ativo = true,
                DataCadastro = DateTime.Now.AddDays(-2),
                DataAlteracao = DateTime.Now,
                Nome = nome,
                Sobrenome = sobrenome,
                DataNascimento = DateTime.Now.AddDays(-3650)
            };
        }

        private static int MockId() => !_pessoas?.Any() ?? true ? 1 : _pessoas.Max(x => x.Id) + 1;
    }
}
