﻿using Dapper;
using Ecommerce.Domain.Repository;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Ecommerce.Infra.Dapper.Interfaces;
using Ecommerce.Domain.Entities.Produtos;

namespace Ecommerce.Infra.Dapper.Repositories
{
    public class FabricanteRepository : Repository<Fabricante>, IFabricanteRepository
    {
        public FabricanteRepository(IConfiguration configuration, IUnitOfWork unitOfWork) : base(configuration, unitOfWork)
        {
        }

        public override void Cadastrar(Fabricante entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            dbConnection.Open();
            var transaction = dbConnection.BeginTransaction();

            try
            {
                var query1 = @"INSERT INTO FABRICANTE values (@Nome, @Ativo, @CNPJ);                          
                          SELECT CAST(SCOPE_IDENTITY() AS INT);";

                entidade.Id = dbConnection.Query<int>(query1, entidade, transaction).Single();

                if (entidade.Endereco != null)
                {
                    entidade.Endereco.EntidadeId = entidade.Id;

                    var query2 = @"INSERT INTO ENDERECO values (@Logradouro, @Numero, @CEP, @Bairro, @Cidade, @Estado, @EntidadeId)
                               SELECT CAST(SCOPE_IDENTITY() AS INT); ";

                    entidade.Endereco.Id = dbConnection.Query<int>(query2, entidade.Endereco, transaction).Single();
                }
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                dbConnection.Close(); 
            }
                        
        }

        public override void Alterar(Fabricante entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            dbConnection.Open();
            var transaction = dbConnection.BeginTransaction();



            try
            {
                var query1 = "UPDATE FABRICANTE SET Nome=@Nome, Ativo=@Ativo, CNPJ=@CNPJ where Id=Id";
                dbConnection.Execute(query1, entidade, transaction);

                if (entidade.Endereco != null)
                {
                    var query2 = "UPDATE ENDERECO SET Logradouro=@Logradouro, Numero=@Numero, CEP=@CEP, Bairro=@Bairro, Cidade=@Cidade, Estado=@Estado where Id=Id";
                    dbConnection.Execute(query2, entidade.Endereco, transaction);
                }
                

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public override void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public override Fabricante ObterPorId(int id)
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            var query = "SELECT * FROM FABRICANTE F LEFT JOIN ENDERECO E ON F.ID=E.ENTIDADEID WHERE F.ID=@Id";

            return dbConnection.Query<Fabricante, Endereco, Fabricante>(sql : query,
                (fabricante, endereco) => 
                {
                    fabricante.Endereco = endereco;

                    return fabricante;
                }
                ,param: new {Id = id }).SingleOrDefault();

        }

        public override IList<Fabricante> ObterTodos()
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            var query = "SELECT * FROM FABRICANTE F LEFT JOIN ENDERECO E ON F.ID=E.ENTIDADEID";

            return dbConnection.Query<Fabricante, Endereco, Fabricante>(sql: query,
                (fabricante, endereco) =>
                {
                    fabricante.Endereco = endereco;

                    return fabricante;
                }).ToList();
        }
    }
}
