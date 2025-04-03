using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Infrastructure.Loggers
{
    public sealed class ConsoleLoggerProvider : ILoggerProvider
    {
        private readonly LogLevel _logLevel;
        private readonly ConsoleColor _color;

        public ConsoleLoggerProvider(LogLevel logLevel, ConsoleColor color)
        {
            _logLevel = logLevel;
            _color = color;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new ConsoleLogger(categoryName, _logLevel, _color);
        }

        public void Dispose()
        {
            // Nothing to dispose in this case
        }
    }
}
