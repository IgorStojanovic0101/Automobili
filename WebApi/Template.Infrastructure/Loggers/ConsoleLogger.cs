using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Infrastructure.Loggers
{
    public sealed class ConsoleLogger(string categoryName, LogLevel logLevel, ConsoleColor color) : ILogger
    {
        private readonly string CategoryName = categoryName;
        private readonly LogLevel LogLevel = logLevel;
        private readonly ConsoleColor Color = color;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
           
            if (!IsEnabled(logLevel))
                return;

            Console.ForegroundColor = Color;
            Console.WriteLine(formatter(state, exception));
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            if (logLevel == LogLevel.None)
                return false;
            return logLevel >= LogLevel;
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            var scope = new Scope<TState>(state);

            return scope;
        }

        public class Scope<TState> : IDisposable
        {
            private readonly TState _state;

            public Scope(TState state)
            {
                _state = state;
            }

            public void Dispose()
            {
                // Clean up resources or handle the scope cleanup logic here
                Console.WriteLine($"Disposing scope with state: {_state}");
            }
        }
    }
}
