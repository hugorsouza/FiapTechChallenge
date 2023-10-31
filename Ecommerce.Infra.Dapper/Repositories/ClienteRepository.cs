using Dapper;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
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
        const string sql = @"SELECT 
                c.*, 
                u.* 
            FROM Cliente c
            JOIN Usuario u on c.Id = u.Id
            ";
        return Connection.Query<Cliente, Usuario, Cliente>(sql, (cliente, usuario) =>
            {
                cliente.Usuario ??= usuario;
                usuario.Cliente ??= cliente;
                return cliente;
            }, 
            splitOn: "Id", 
            transaction: Transaction).ToList();
    }

    public override void Alterar(Cliente entidade)
    {
        const string sql = @"
            UPDATE Cliente
            SET
                Nome = @Nome
                ,Sobrenome = @Sobrenome
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
                c.DataAlteracao = @Agora
            FROM Cliente c 
            WHERE c.Id = @Id
            
            UPDATE u 
            SET 
                u.DataAlteracao = @Agora,
                u.Ativo = 0
            FROM Usuario u 
            WHERE u.Id = @Id
            ";
        Connection.Execute(NovoComando(sql, new { Id = id, Agora = DateTime.Now }));
    }

    public override Cliente ObterPorId(int id)
    {
        const string sql = @"SELECT TOP 1
                c.*, 
                u.* 
            FROM Cliente c
            JOIN Usuario u on c.Id = u.Id
            WHERE c.Id = @Id";
        return Connection.Query<Cliente, Usuario, Cliente>(sql, (cliente, usuario) =>
            {
                cliente.Usuario ??= usuario;
                usuario.Cliente ??= cliente;
                return cliente;
            }, 
            param: new {Id = id},
            splitOn: "Id", 
            transaction: Transaction).FirstOrDefault();
    }
    
    public Cliente ObterPorCpf(string cpf)
    {
        const string sql = @"SELECT TOP 1
                c.*, 
                u.* 
            FROM Cliente c
            JOIN Usuario u on c.Id = u.Id
            WHERE c.Cpf = @Cpf";
        return Connection.Query<Cliente, Usuario, Cliente>(sql, (cliente, usuario) =>
            {
                cliente.Usuario ??= usuario;
                usuario.Cliente ??= cliente;
                return cliente;
            }, 
            param: new {Cpf = cpf},
            splitOn: "Id", 
            transaction: Transaction).FirstOrDefault();
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