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

        public CustomLoggerProvider(CustomLoggerProviderConfiguration loggerConfig)
        {
            _loggerConfig= loggerConfig;
        }
        public ILogger CreateLogger(string categoria)
        {
            return loggers.GetOrAdd(categoria, nome => new CustomLogger(nome, _loggerConfig));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
