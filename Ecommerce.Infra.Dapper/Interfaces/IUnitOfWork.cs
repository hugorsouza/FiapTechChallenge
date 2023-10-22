using System.Data;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Infra.Dapper.Interfaces;

public interface IUnitOfWork : ITransactionService
{
    IDbConnection Connection { get; }
    IDbTransaction Transaction { get; }
    void BeginTransaction();
    void Commit();
    void Rollback();
    bool TryRollback();
    void Open();
    void Close();
    void Dispose();
}