using Ecommerce.Domain.Interfaces.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Ecommerce.Infra.Dapper.Interfaces;

namespace Ecommerce.Infra.Dapper.Repositories
{
    public abstract class Repository : IRepository
    {
        private readonly string _connectionString;
        protected string ConnectionString => _connectionString;
        private readonly IUnitOfWork _unitOfWork;
        protected IDbConnection Connection => _unitOfWork.Connection;
        protected IDbTransaction Transaction => _unitOfWork.Transaction;

        public void BeginTransaction()
        {
            _unitOfWork.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _unitOfWork.Commit();
        }

        public void RollbackTransaction()
        {
            _unitOfWork.Rollback();
        }
        
        protected Repository(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _connectionString = configuration.GetConnectionString("Ecommerce");
            _unitOfWork = unitOfWork;
        }
        
        protected CommandDefinition NovoComando(string sql, object parametros = null, CancellationToken cancellationToken = default)
        {
            //Utilizará uma transaction automaticamente caso haja alguma
            return new CommandDefinition(sql, parametros, transaction: Transaction, cancellationToken: cancellationToken);
        }
    } 

    public abstract class Repository<T> : Repository, IRepository<T> where T : class
    {
        protected Repository(IConfiguration configuration, IUnitOfWork unitOfWork) : base(configuration, unitOfWork)
        {
            
        }

        public abstract void Alterar(T entidade);
        public abstract void Cadastrar(T entidade);
        public abstract void Deletar(int id);
        public abstract T ObterPorId(int id);
        public abstract IList<T> ObterTodos();

    }
}
