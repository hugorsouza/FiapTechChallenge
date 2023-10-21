using Dapper;
using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Repository;
using Ecommerce.Domain.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Infra.Dapper.Interfaces;

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
                var query1 = @"INSERT INTO FABRICANTE values (@Id, @Nome, @Ativo, @CNPJ);                          
                          SELECT CAST(SCOPE_IDENTITY() AS INT);";

                entidade.Id = dbConnection.Query<int>(query1, entidade, transaction).Single();

                if (entidade.Endereco != null)
                {
                    entidade.Endereco.IdEntidade = entidade.Id;

                    var query2 = @"INSERT INTO ENDERECO values (@Logradouro, @Numero, @CEP, @Bairro, @Cidade, @Estado, @IdEntidade)
                               SELECT CAST(SCOPE_IDENTITY() AS INT); ";

                    entidade.Endereco.Id = dbConnection.Query<int>(query2, entidade.Endereco, transaction).Single();
                }
                transaction.Commit();
            }
            catch (Exception)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception)
                {
                    //TODO TRATAR A MENSAGEM PARA A CONTROLLER
                    throw;
                }
                
            }
            finally
            {
                dbConnection.Close(); 
            }
                        
        }

        public override void Alterar(Fabricante entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);

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
                try
                {
                    transaction.Rollback();
                }
                catch (Exception)
                {
                    //TODO - TRATAR A EXCEÇÃO
                    throw;
                }
            }finally
            {
                dbConnection.Close();
            }
        }

        public override void Deletar(int id)
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            var transaction = dbConnection.BeginTransaction();



            try
            {
                var query1 = "DELETE FROM ENDERECO WHERE EntidadeId=@EntidadeId";
                dbConnection.Execute(query1, new { EntidadeId = id }, transaction);

                //TODO IMPLEMENTAÇÃO DO REPOSITORIO
                // var query2 = "UPDATE ENDERECO SET Logradouro=@Logradouro, Numero=@Numero, CEP=@CEP, Bairro=@Bairro, Cidade=@Cidade, Estado=@Estado where Id=Id";
                // dbConnection.Execute(query1, entidade.Endereco, transaction);



                transaction.Commit();
            }
            catch (Exception)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception)
                {
                    //TODO - TRATAR A EXCEÇÃO
                    throw;
                }
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public override Fabricante ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        public override IList<Fabricante> ObterTodos()
        {
            throw new NotImplementedException();
        }
    }
}
