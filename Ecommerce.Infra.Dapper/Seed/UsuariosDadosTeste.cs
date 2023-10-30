using Bogus;
using Bogus.Extensions.Brazil;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Fisica;
using Ecommerce.Domain.Entities.Pessoas.Juridica;
using Ecommerce.Domain.Entities.Shared;

namespace Ecommerce.Infra.Dapper.Seed
{
    internal static class UsuariosDadosTeste
    {
        private const string TesteSenha = "$2a$12$UtM3mPfEyKfkyahgAO/y4erVhvQk4ShIJPfCLaTkwwP60ueaufHbK";//123456
        private const string EmailTesteCliente = "cliente@hotmail.com";
        private const string EmailTesteFuncionarioAdmin = "admin@hotmail.com";
        private const string EmailTesteFuncionarioPadrao = "funcionario@hotmail.com";
        
        #region Fakers
        
        private static readonly Faker<Usuario> FakerUsuario = new Faker<Usuario>("pt_BR")
            .RuleFor(x => x.Email, f => f.Person.Email.ToLowerInvariant())
            .RuleFor(x => x.NomeExibicao, f => f.Person.FullName)
            .RuleFor(x => x.Senha, f => TesteSenha)
            .RuleFor(x => x.DataCadastro, f => f.Date.Past(3))
            .RuleFor(x => x.Ativo, _ => true)
            .RuleFor(x => x.DataAlteracao, f => f.Date.Soon());
        
        private static readonly Faker<Cliente> FakerCliente = new Faker<Cliente>("pt_BR")
            .RuleFor(x => x.Cpf, f => f.Person.Cpf(includeFormatSymbols: false))
            .RuleFor(x => x.Nome, f => f.Person.FirstName)
            .RuleFor(x => x.Sobrenome, f => f.Person.LastName)
            .RuleFor(x => x.DataNascimento, f => f.Person.DateOfBirth)
            .RuleFor(x => x.DataCadastro, f => f.Date.Past(3))
            .RuleFor(x => x.DataAlteracao, f => f.Date.Soon())
            .RuleFor(x => x.DataCadastro, f => f.Date.Past(3))
            .RuleFor(x => x.DataAlteracao, f => f.Date.Soon())
            .RuleFor(x => x.Usuario, _ => FakerUsuario.Generate());
        
        private static readonly Faker<Funcionario> FakerFuncionario = new Faker<Funcionario>("pt_BR")
            .RuleFor(x => x.Cpf, f => f.Person.Cpf(includeFormatSymbols: false))
            .RuleFor(x => x.Nome, f => f.Person.FirstName)
            .RuleFor(x => x.Sobrenome, f => f.Person.LastName)
            .RuleFor(x => x.DataNascimento, f => f.Person.DateOfBirth)
            .RuleFor(x => x.DataCadastro, f => f.Date.Past(3))
            .RuleFor(x => x.DataAlteracao, f => f.Date.Soon())
            .RuleFor(x => x.DataCadastro, f => f.Date.Past(3))
            .RuleFor(x => x.DataAlteracao, f => f.Date.Soon())
            .RuleFor(x => x.Cargo, _ => "Tester")
            .RuleFor(x => x.Usuario, _ => FakerUsuario.Generate());
        
        #endregion

        static UsuariosDadosTeste()
        {
        }

        private static void SetEmail<TUsuario>(string email, TUsuario usuario) where TUsuario : IUsuario
        {
            usuario.Usuario.Email = email.ToLowerInvariant();
            usuario.Usuario.EmailNormalizado = email.Trim().ToUpperInvariant();
            if (usuario is Funcionario func &&
                string.Equals(email, EmailTesteFuncionarioAdmin, StringComparison.InvariantCultureIgnoreCase))
                func.Administrador = true;
        }
        
        private static TPessoa GerarNovo<TPessoa>(Faker<TPessoa> faker, string? email = null)
            where TPessoa : PessoaFisica
        {
            var pessoa = faker.Generate();
            pessoa.Usuario.NomeExibicao = $"{pessoa.Nome} {pessoa.Sobrenome}";
            pessoa.Usuario.Perfil = pessoa switch
            {
                Cliente => PerfilUsuario.Cliente,
                Funcionario => PerfilUsuario.Funcionario,
                _ => pessoa.Usuario.Perfil
            };

            if (email is null) return pessoa;
            
            SetEmail(email, pessoa);
            return pessoa;
        }
        
        public static readonly Cliente[] Clientes = Enumerable.Range(1, 10)
            .Select(_ => GerarNovo(FakerCliente))
            .Union(new[]
            {
               GerarNovo(FakerCliente, EmailTesteCliente)
            }).ToArray();
        
        public static readonly Funcionario[] Funcionarios = Enumerable.Range(1, 10)
            .Select(_ => GerarNovo(FakerFuncionario))
            .Union(new[]
            {
                GerarNovo(FakerFuncionario, EmailTesteFuncionarioAdmin)
            }).Union(new[]
            {
                GerarNovo(FakerFuncionario, EmailTesteFuncionarioPadrao)
            }).ToArray();
    }
}
