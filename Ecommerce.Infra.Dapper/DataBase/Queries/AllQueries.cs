namespace Ecommerce.Infra.Dapper.DataBase.Queries
{
    public static class AllQueries 
    {
		private static string GetQuery(string propertyName = null)
        {
            var fileName = $"Ecommerce.Infra.Dapper.DataBase.Queries.{propertyName}.sql";

            var stream = typeof(AllQueries).Assembly.GetManifestResourceStream(fileName);

            if (stream == null) throw new Exception($"The file {propertyName}.sql was not found in Ecommerce.Infra.Dapper.DataBase.Queries");

            using (var sr = new StreamReader(stream))
            {
                return sr.ReadToEnd(); 
            }
        } 
        private static string _fazerPedido;
        
        public static string FazerPedido
        {
            get
            {
                if (_fazerPedido is null)
                    _fazerPedido = GetQuery();
                return _fazerPedido;
            }
            set { _fazerPedido = value;}
        }
    }

    
}
