using System.Data;
using System.Data.SqlClient;
using Ecommerce.Infra.Dapper.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Infra.Dapper.Factory;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;
    
    public DbConnectionFactory(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("Ecommerce");
        ArgumentNullException.ThrowIfNull(_connectionString);
    }
    
    public IDbConnection Create()
    {
        return CreateConnection(_connectionString);
    }
    
    private static IDbConnection CreateConnection(string connectionString)
    {
        return new SqlConnection(connectionString);
    }
}