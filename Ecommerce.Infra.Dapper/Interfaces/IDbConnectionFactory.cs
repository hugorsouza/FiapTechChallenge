using System.Data;

namespace Ecommerce.Infra.Dapper.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection Create();
}