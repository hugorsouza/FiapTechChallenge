//using Microsoft.Azure.Cosmos.Table;


using Azure.Data.Tables;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infra.Logging.Logging
{
    public class CustomLogger : ILogger
    {
        private readonly string _loggerName;
        private readonly CustomLoggerProviderConfiguration _configuration;
        private readonly IConfiguration _config;

        public CustomLogger(string nome, CustomLoggerProviderConfiguration configuration, IConfiguration config)
        {
            _loggerName= nome;
            _configuration= configuration;       
            _config= config;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception? exception, 
            Func<TState, Exception?, string> formatter)
        {
            var entity = new LogEntity
            {
                PartitionKey = Guid.NewGuid().ToString(),
                RowKey = "ILogger",
                Timestamp = DateTime.Now,
                LogLevel = logLevel.ToString(),
                Mensagem = string.Format($"{DateTime.Now} - {logLevel}:{eventId} " +
                    $"- {formatter(state, exception)}")
            };

            LogarNoTableStorage(entity);           
        }

       

        private async Task LogarNoTableStorage(LogEntity entity)
        {
            await UpsertEntityAsync(entity);
        }

        public async Task<LogEntity> UpsertEntityAsync(LogEntity entity)
        {
            var tableClient = await GetTableClient();
            await tableClient.UpsertEntityAsync(entity);

            return entity;
        }

        private async Task<TableClient> GetTableClient()
        {
            var serviceClient = new TableServiceClient(_config.GetSection("BlobStorage:ConectionString").Value);
            var tableClient = serviceClient.GetTableClient(_config.GetSection("BlobStorage:Table").Value);

            await tableClient.CreateIfNotExistsAsync();
            return tableClient;
        }


    }
}
