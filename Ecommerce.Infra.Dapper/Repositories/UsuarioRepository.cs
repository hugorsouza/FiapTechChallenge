using Dapper;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Dapper.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Infra.Dapper.Repositories;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(IConfiguration configuration, IUnitOfWork unitOfWork) : base(configuration, unitOfWork)
    {
    }

    public override IList<Usuario> ObterTodos()
    {
        const string sql = "SELECT * FROM Usuario WHERE Ativo = 1";
        return Connection.Query<Usuario>(NovoComando(sql)).ToList();
    }

    public override void Alterar(Usuario entidade)
    {
        const string sql = @"
            UPDATE Cliente
            SET
                Nome = @Nome
                ,Sobrenome = @Sobrenome
                ,Cpf = @Cpf
                ,DataNascimento = @DataNascimento
                ,DataAlteracao = GETDATE()
                ,RecebeNewsletterEmail = @RecebeNewsletterEmail
            WHERE Id = @Id
        ";
        var parametros = new
        {
            entidade.Id,
            entidade.NomeExibicao,
            entidade.Email,
            entidade.Senha,
            DataCadastroUtc = entidade.DataCadastro,
            DataAlteracaoUtc = entidade.DataAlteracao,
            entidade.Perfil,
            entidade.EmailConfirmado
        };
        Connection.Execute(NovoComando(sql, parametros));
    }

    public override void Deletar(int id)
    { 
        const string sql = "UPDATE Usuario SET Ativo = 0 WHERE Id = @Id";
        Connection.Execute(NovoComando(sql, new { Id = id }));
    }

    public override Usuario ObterPorId(int id)
    {
        const string sql = "SELECT * FROM Usuario WHERE Id = @Id";
        return Connection.QueryFirst<Usuario>(NovoComando(sql, new {Id = id}));
    }
    
    public Usuario ObterUsuarioPorEmail(string email)
    {
        email = email?.Trim().ToUpperInvariant();
        const string sql = "SELECT * FROM Usuario WHERE EmailNormalizado = @Email";
        return Connection.QueryFirst<Usuario>(NovoComando(sql, new {Email = email}));
    }

    public override void Cadastrar(Usuario entidade)
    {
        throw new NotImplementedException();
    }

    public int CadastrarObterId(Usuario entidade)
    {
        const string sql = @"
           INSERT INTO Usuario
               (NomeExibicao ,Email ,Senha
		       ,DataCadastro ,Perfil
               ,EmailConfirmado ,Ativo)
             VALUES (
			        @NomeExibicao ,@Email ,@Senha
                   ,@DataCadastro ,@Perfil
                   ,@EmailConfirmado ,@Ativo
	        )
           
           SELECT SCOPE_IDENTITY()
        ";
        var parametros = new
        {
            entidade.NomeExibicao,
            entidade.Email,
            entidade.Senha,
            entidade.DataCadastro,
            entidade.Perfil,
            entidade.EmailConfirmado,
            entidade.Ativo
        };
        return Connection.QuerySingle<int>(NovoComando(sql, parametros));
    }
}