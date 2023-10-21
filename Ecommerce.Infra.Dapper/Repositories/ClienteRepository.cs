using System.Data.SqlClient;
using Dapper;
using Ecommerce.Domain.Entities.Pessoas.Fisica;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Dapper.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Infra.Dapper.Repositories;

public class ClienteRepository : Repository<Cliente>, IClienteRepository
{
    public ClienteRepository(IConfiguration configuration, IUnitOfWork unitOfWork) : base(configuration, unitOfWork)
    {
    }

    public override IList<Cliente> ObterTodos()
    {
        const string sql = "SELECT * FROM Cliente";
        return Connection.Query<Cliente>(NovoComando(sql)).ToList();
    }

    public override void Alterar(Cliente entidade)
    {
        const string sql = @"
            UPDATE Cliente
            SET
                Nome = @Nome
                ,Sobrenome = @Sobrenome
                ,Cpf = @Cpf
                ,DataNascimento = @DataNascimento
                ,DataAlteracao = @DataAlteracao
                ,RecebeNewsletterEmail = @RecebeNewsletterEmail
            WHERE Id = @Id
        ";
        var parametros = new
        {
            entidade.Id,
            entidade.Nome,
            entidade.Sobrenome,
            entidade.Cpf,
            entidade.DataNascimento,
            entidade.DataAlteracao,
            entidade.RecebeNewsletterEmail
        };
        Connection.Execute(NovoComando(sql, parametros));
    }

    public override void Deletar(int id)
    { 
        const string sql = @"
            UPDATE c 
            SET 
                c.DataAlteracao = GETDATE()
            FROM Cliente c 
            WHERE c.Id = @Id
            
            UPDATE u 
            SET 
                u.DataAlteracao = GETDATE(),
                u.Ativo = 0
            FROM Usuario u 
            WHERE u.Id = @Id
            ";
        Connection.Execute(NovoComando(sql, new { Id = id }));
    }

    public override Cliente ObterPorId(int id)
    {
        const string sql = "SELECT * FROM Cliente WHERE Id = @Id";
        return Connection.QueryFirst<Cliente>(NovoComando(sql, new {Id = id}));
    }

    public override void Cadastrar(Cliente entidade)
    {
        const string sql = @"
            INSERT INTO Cliente
                (Id ,Nome ,Sobrenome
                ,Cpf ,DataNascimento ,DataCadastro
                ,RecebeNewsletterEmail)
            VALUES (
               @Id, @Nome, @Sobrenome
               ,@Cpf, @DataNascimento, @DataCadastro
               ,@RecebeNewsletterEmail
            )
        ";
        var parametros = new
        {
            entidade.Id,
            entidade.Nome,
            entidade.Sobrenome,
            entidade.Cpf,
            entidade.DataNascimento,
            entidade.DataCadastro,
            entidade.RecebeNewsletterEmail
        };
        Connection.Execute(NovoComando(sql, parametros));
    }
}