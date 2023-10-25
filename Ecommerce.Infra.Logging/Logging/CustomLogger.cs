using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infra.Logging.Logging
{
    public class CustomLogger : ILogger
    {
        private readonly string _loggerName;
        private readonly CustomLoggerProviderConfiguration _configuration;

        public CustomLogger(string nome, CustomLoggerProviderConfiguration configuration)
        {
            _loggerName= nome;
            _configuration= configuration;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var mensagem = string.Format($"{DateTime.Now} - {logLevel}:{eventId} " +
                $"- {formatter(state, exception)}");

            LogarNoTableStorage(mensagem);
        }

        private void LogarNoTableStorage(string mensagem)
        {
            var caminhoDOArquivo = @$"C:\Projetos\FIAP\FiapTechChallenge\Log-{DateTime.Now:yyyy-MM-dd}.txt";

            if (!File.Exists(caminhoDOArquivo)) 
            {
                Directory.CreateDirectory(Path.GetDirectoryName(caminhoDOArquivo));
                File.Create(caminhoDOArquivo).Dispose();
            }

            using StreamWriter streamWriter = new StreamWriter(caminhoDOArquivo, true);

            streamWriter.WriteLine(mensagem);

            streamWriter.Close();
        }
    }
}
