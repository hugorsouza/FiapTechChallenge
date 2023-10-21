using Bogus;
using Bogus.Extensions.Brazil;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Fisica;
using Ecommerce.Domain.Entities.Pessoas.Juridica;
using Ecommerce.Domain.Entities.Shared;

namespace Ecommerce.Infra.Dapper.DataBase.Seed
{
    internal static class UsuariosDadosTeste
    {
        private const string TesteSenha = "$2a$12$0ttSqbAV5ckzSv/suZLxfeNqjMUbxVAfjmtRcWWrmM0e.g2pdenUy";//12345678
        private const string EmailTesteCliente = "cliente@hotmail.com";
        private const string EmailTesteFuncionario = "admin@hotmail.com";
        private const string EmailTesteEmpresa = "bill.gates@microsoft.com";
        
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

        private static readonly Faker<Empresa> FakerEmpresa = new Faker<Empresa>("pt_BR")
            .RuleFor(x => x.Cnpj, f => f.Company.Cnpj(false))
            .RuleFor(x => x.NomeFantasia, f => f.Company.CompanyName(0))
            .RuleFor(x => x.EmailContato, f => f.Internet.Email())
            .RuleFor(x => x.RazaoSocial, f => f.Company.CompanyName(2))
            .RuleFor(x => x.Usuario, _ => FakerUsuario.Generate());

        #endregion

        static UsuariosDadosTeste()
        {
        }

        private static void SetEmail<TUsuario>(string email, TUsuario usuario) where TUsuario : IUsuario
        {
            usuario.Usuario.Email = email.ToLowerInvariant();
            usuario.Usuario.EmailNormalizado = email.Trim().ToUpperInvariant();
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
        
        private static Empresa GerarNovo(Faker<Empresa> faker, string? email = null)
        {
            var empresa = faker.Generate();
            empresa.Usuario.Perfil = PerfilUsuario.EmpresaTerceira;
            
            if (email is null) return empresa;
            
            SetEmail(email, empresa);
            return empresa;
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
                GerarNovo(FakerFuncionario, EmailTesteFuncionario)
            }).ToArray();
        
        public static readonly Empresa[] Empresas = Enumerable.Range(1, 10)
            .Select(_ => GerarNovo(FakerEmpresa))
            .Union(new[]
            {
                GerarNovo(FakerEmpresa, EmailTesteEmpresa)
            }).ToArray();
    }
}
