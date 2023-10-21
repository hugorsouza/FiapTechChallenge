using System.Data;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Infra.Dapper.Interfaces;

public interface IUnitOfWork : ITransactionService
{
    IDbConnection Connection { get; }
    IDbTransaction Transaction { get; }
}