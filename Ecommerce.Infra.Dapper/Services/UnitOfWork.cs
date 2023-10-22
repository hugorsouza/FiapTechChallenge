using System.Data;
using Ecommerce.Infra.Dapper.Interfaces;

namespace Ecommerce.Infra.Dapper.Services;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly Lazy<IDbConnection> _connection;
    private IDbTransaction _transaction;
    public IDbConnection Connection => _connection.Value;

    public IDbTransaction Transaction => _transaction;

    public UnitOfWork(IDbConnectionFactory connectionFactory)
    {
        _connection = new Lazy<IDbConnection>(() =>
        {
            var connection = connectionFactory.Create();
            return connection;
        });
    }

    public void BeginTransaction()
    {
        if (_transaction != null)
        {
            throw new InvalidOperationException("Já existe uma transação em andamento.");
        }
        Connection.Open();
        _transaction = Connection.BeginTransaction();
    }

    public void Commit()
    {
        try
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Nenhuma transação em andamento.");
            }

            _transaction.Commit();
            _transaction = null;
        }
        finally
        {
            Close();
        }
    }

    public void Rollback()
    {
        try
        {
            if (_transaction is null)
                return;

            _transaction.Rollback();
            _transaction = null;
        }
        finally
        {
            Close();
        }
    }

    public bool TryRollback()
    {
        try
        {
            Rollback();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void Open()
    {
        if(Connection.State == ConnectionState.Broken)
            Connection.Close();
        Connection.Open();
    }
    
    public void Close()
    {
        Connection?.Close();
    }

    public void Dispose()
    {
        if (_transaction != null)
        {
            _transaction.Dispose();
            _transaction = null;
        }

        if (_connection.IsValueCreated)
        {
            _connection.Value.Dispose();
        }
        GC.SuppressFinalize(this);
    }
}