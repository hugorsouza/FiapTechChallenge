 
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Ecommerce.Infra.Dapper.DataBase.Queries
{
    public static class AllQueries 
    {
		private static string GetQuery([CallerMemberName]string propertyName = null)
        {
            var fileName = $"Ecommerce.Infra.Dapper.DataBase.Queries.{propertyName}.sql";

            var stream = typeof(AllQueries).Assembly.GetManifestResourceStream(fileName);

            if (stream == null) throw new Exception($"The file {propertyName}.sql was not found in Ecommerce.Infra.Dapper.DataBase.Queries");

            using (var sr = new StreamReader(stream))
            {
                return sr.ReadToEnd(); 
            }
        } 

    }
}
