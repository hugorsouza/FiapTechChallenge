
using Azure;
using Azure.Data.Tables;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infra.Logging.Logging
{
    public class LogEntity : ITableEntity
    {
        public string LogLevel { get; set; }
        public string Mensagem { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }

}
