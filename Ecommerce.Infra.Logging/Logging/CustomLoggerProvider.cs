using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infra.Logging.Logging
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        private readonly CustomLoggerProviderConfiguration _loggerConfig;
        private readonly ConcurrentDictionary<string, CustomLogger> loggers = new ConcurrentDictionary<string, CustomLogger>();
        private readonly IConfiguration _configuration;

        public CustomLoggerProvider(CustomLoggerProviderConfiguration loggerConfig, IConfiguration config)
        {
            _loggerConfig = loggerConfig;
            _configuration= config;
        }
        public ILogger CreateLogger(string categoria)
        {
           return loggers.GetOrAdd(categoria, nome => new CustomLogger(nome, _loggerConfig, _configuration));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
