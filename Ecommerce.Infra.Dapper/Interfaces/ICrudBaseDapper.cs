using Dapper;
using System.Data;

namespace Ecommerce.Infra.Dapper.Interfaces
{
    public interface ICrudBaseDapper<TEntity> where TEntity : class
    {
        List<T> ExecuteScriptWithTransaction<T>(string commandText, DynamicParameters parameters, CommandType cmdType = CommandType.Text);
        List<T> ExecuteScriptWithoutTransaction<T>(string commandText, DynamicParameters parameters, CommandType cmdType = CommandType.Text);

        T ExecuteScriptWithTransactionNoList<T>(string commandText, DynamicParameters parameters, CommandType cmdType = CommandType.Text);

        T ExecuteScriptWithoutTransactionNoList<T>(string commandText, DynamicParameters parameters, CommandType cmdType = CommandType.Text);

        void ExecuteScriptWithoutTransactionNoList(string commandText, DynamicParameters parameters, CommandType cmdType = CommandType.Text);
    }
}
