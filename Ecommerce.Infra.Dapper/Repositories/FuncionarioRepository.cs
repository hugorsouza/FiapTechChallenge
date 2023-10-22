using Dapper;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Fisica;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Dapper.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Infra.Dapper.Repositories;

public class FuncionarioRepository : Repository<Funcionario>, IFuncionarioRepository
{
    public FuncionarioRepository(IConfiguration configuration, IUnitOfWork unitOfWork) : base(configuration, unitOfWork)
    {
    }

    public override IList<Funcionario> ObterTodos()
    {
        const string sql = @"SELECT 
                c.*, 
                u.* 
            FROM Funcionario c
            JOIN Usuario u on c.Id = u.Id
            ";
        return Connection.Query<Funcionario, Usuario, Funcionario>(sql, (funcionario, usuario) =>
            {
                funcionario.Usuario ??= usuario;
                usuario.Funcionario ??= funcionario;
                return funcionario;
            }, 
            splitOn: "Id", 
            transaction: Transaction).ToList();
    }

    public override void Alterar(Funcionario entidade)
    {
        const string sql = @"
            UPDATE Funcionario
            SET
                Nome = @Nome
                ,Sobrenome = @Sobrenome
                ,DataNascimento = @DataNascimento
                ,DataAlteracao = @DataAlteracao
                ,Cargo = @Cargo
            
            WHERE Id = @Id
        ";
        var parametros = new
        {
            entidade.Id,
            entidade.Nome,
            entidade.Sobrenome,
            entidade.DataNascimento,
            entidade.DataAlteracao,
            entidade.Cargo
        };
        Connection.Execute(NovoComando(sql, parametros));
    }

    public override void Deletar(int id)
    { 
        const string sql = @"
            UPDATE c 
            SET 
                c.DataAlteracao = @Agora
            FROM Funcionario c 
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

    public override Funcionario ObterPorId(int id)
    {
        const string sql = "SELECT * FROM Funcionario WHERE Id = @Id";
        return Connection.QueryFirst<Funcionario>(NovoComando(sql, new {Id = id}));
    }

    public override void Cadastrar(Funcionario entidade)
    {
        const string sql = @"
            INSERT INTO Funcionario
                (Id ,Nome ,Sobrenome
                ,Cpf ,DataNascimento ,DataCadastro
                ,Cargo ,Administrador)
            VALUES (
               @Id, @Nome, @Sobrenome
               ,@Cpf, @DataNascimento, @DataCadastro
               ,@Cargo ,@Administrador 
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
            entidade.Cargo,
            Administrador = false
        };
        Connection.Execute(NovoComando(sql, parametros));
    }
}