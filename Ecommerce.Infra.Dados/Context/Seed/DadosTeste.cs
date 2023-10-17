using Bogus;
using Bogus.Extensions.Brazil;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Autenticacao;

namespace Ecommerce.Infra.Dados.Context.Seed
{
    internal static class DadosTeste
    {
        private const string testeSenha = "$2a$12$0ttSqbAV5ckzSv/suZLxfeNqjMUbxVAfjmtRcWWrmM0e.g2pdenUy";//12345678

        private static readonly Usuario[] Usuarios = new Usuario[]
        {
            new Usuario
            {
                Email = "admin@hotmail.com",
                DataCadastro = DateTime.Now,
                DataAlteracao = DateTime.Now,
                NomeExibicao = "[Teste] Administrador",
                Perfil = PerfilUsuario.Operador,
                Senha = testeSenha
            },
            new Usuario
            {
                Email = "cliente@hotmail.com",
                DataCadastro = DateTime.Now,
                DataAlteracao = DateTime.Now,
                NomeExibicao = "[Teste] Cliente",
                Perfil = PerfilUsuario.Cliente,
                Senha = testeSenha
            },
        };

        private static Faker<Usuario> _fakerUsuario = new Faker<Usuario>()
            .RuleFor(x => x.Email, f => f.Person.Email)
            .RuleFor(x => x.NomeExibicao, f => f.Person.FullName)
            .RuleFor(x => x.Perfil,  f => f.Random.Enum<PerfilUsuario>(exclude: PerfilUsuario.Terceiros))
            .RuleFor(x => x.Senha, f => testeSenha)
            .RuleFor(x => x.DataCadastro, f => f.Date.Past(3))
            .RuleFor(x => x.DataAlteracao, f => f.Date.Soon());

        private static Faker<PessoaFisica> _fakerPessoa = new Faker<PessoaFisica>()
            .RuleFor(x => x.Cpf, f => f.Person.Cpf(includeFormatSymbols: false))
            .RuleFor(x => x.Nome, f => f.Person.FirstName)
            .RuleFor(x => x.Sobrenome, f => f.Person.LastName)
            .RuleFor(x => x.Ativo, f => true)
            .RuleFor(x => x.DataNascimento, f => f.Person.DateOfBirth)
            .RuleFor(x => x.DataCadastro, f => f.Date.Past(3))
            .RuleFor(x => x.DataAlteracao, f => f.Date.Soon())
            .RuleFor(x => x.Usuario, _ => _fakerUsuario.Generate());

        public static readonly PessoaFisica[] Pessoas = Usuarios.Select(usuario =>
        {
            var pessoa = _fakerPessoa.Generate();
            pessoa.Usuario = usuario;
            return pessoa;
        }).Union(Enumerable.Range(1, 100).Select(index =>
        {
            var pessoa = _fakerPessoa.Generate();
            return pessoa;
        })).ToArray();
    }
}
