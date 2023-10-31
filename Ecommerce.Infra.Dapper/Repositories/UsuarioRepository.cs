using Dapper;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Fisica;
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
            UPDATE Usuario
            SET
                NomeExibicao = @NomeExibicao
                ,DataAlteracao = @DataAlteracao
                ,EmailConfirmado = @EmailConfirmado
                ,Ativo = @Ativo
            WHERE Id = @Id
        ";
        Connection.Execute(NovoComando(sql, entidade));
    }
    
    public void AlterarSenha(Usuario entidade)
    {
        const string sql = @"
            UPDATE Usuario
            SET
                Senha = @Senha
            WHERE Id = @Id
        ";
        var parametros = new
        {
            entidade.Id,
            entidade.Senha,
            entidade.DataAlteracao
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
        return Connection.QueryFirstOrDefault<Usuario>(NovoComando(sql, new {Id = id}));
    }
    
    public Usuario ObterUsuarioPorEmail(string email)
    {
        email = email?.Trim().ToUpperInvariant();
        const string sql = @"
            SELECT 
                U.*, 
                C.*,
                F.*
            FROM Usuario U
            LEFT JOIN dbo.Cliente C on U.Id = C.Id
            LEFT JOIN dbo.Funcionario F on U.Id = F.Id
            WHERE 
                u.EmailNormalizado = @Email";
        return Connection.Query<Usuario, Cliente, Funcionario, Usuario>(sql, 
            map: (usuario, cliente, funcionario) =>
            {
                switch (usuario.Perfil)
                {
                    case PerfilUsuario.Cliente:
                        usuario.Cliente ??= cliente;
                        cliente.Usuario ??= usuario;
                        break;
                    case PerfilUsuario.Funcionario:
                        usuario.Funcionario ??= funcionario;
                        funcionario.Usuario ??= usuario;
                        break;
                }
                return usuario;
            }
            ,new {Email = email}
            ,splitOn: "Id, Id",
            transaction: Transaction
            ).FirstOrDefault();
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