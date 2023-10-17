using Dapper;
using Ecommerce.Infra.Dapper.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Ecommerce.Infra.Dapper.Services
{
    public class CrudBaseDapper<TEntity> : ICrudBaseDapper<TEntity> where TEntity : class
    {
        private SqlConnection _connection;
        internal string _connectionString;
        private IConfiguration configuration;

        public CrudBaseDapper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CrudBaseDapper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void OpenConnection()
        {
            _connection = GetConnection(_connectionString);

            if (!(_connection.State == ConnectionState.Open))
                _connection.Open();
        }

        public void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed) _connection.Close();
        }

        public static SqlConnection GetConnection(string connectionString)
        {
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }

        public List<T> ExecuteScriptWithoutTransaction<T>(string commandText, DynamicParameters parameters, CommandType cmdType = CommandType.Text)
        {
            try
            {
                OpenConnection();

                return _connection.Query<T>(commandText, parameters, commandType: cmdType).ToList();
            }
            finally
            {

                CloseConnection();
            }
        }
        public List<T> ExecuteScriptWithTransaction<T>(string commandText, DynamicParameters parameters, CommandType cmdType = CommandType.Text)
        {
            try
            {
                OpenConnection();
                List<T> returnList;

                using (var transaction = _connection.BeginTransaction())
                {
                    returnList = _connection.Query<T>(commandText, parameters, transaction, commandType: cmdType).ToList();
                    transaction.Commit();
                }
                return returnList;
            }
            finally
            {
                CloseConnection();
            }
        }

        public T ExecuteScriptWithoutTransactionNoList<T>(string commandText, DynamicParameters parameters, CommandType cmdType = CommandType.Text)
        {
            try
            {
                OpenConnection();

                return _connection.Query<T>(commandText, parameters, commandType: cmdType).FirstOrDefault();
            }
            finally
            {
                CloseConnection();
            }
        }

        public T ExecuteScriptWithTransactionNoList<T>(string commandText, DynamicParameters parameters, CommandType cmdType = CommandType.Text)
        {
            try
            {
                OpenConnection();
                T returno;

                using (var transaction = _connection.BeginTransaction())
                {
                    returno = _connection.Query<T>(commandText, parameters, transaction, commandType: cmdType).FirstOrDefault();
                    transaction.Commit();
                }

                return returno;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void ExecuteScriptWithoutTransactionNoList(string commandText, DynamicParameters parameters, CommandType cmdType = CommandType.Text)
        {
            try
            {
                OpenConnection();

                using (var transaction = _connection.BeginTransaction())
                {
                    _connection.Execute(commandText, parameters, transaction, commandType: cmdType);
                    transaction.Commit();
                }

            }
            finally
            {
                CloseConnection();
            }
        }

    }
}
