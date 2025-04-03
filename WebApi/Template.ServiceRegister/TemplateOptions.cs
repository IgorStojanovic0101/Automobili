using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Infrastructure.Loggers;

namespace Template.ServiceRegister.ServiceRegister
{

    public sealed class TempalateOptions
    {
        public ILoggerFactory LogFactory { get; set; } = NullLoggerFactory.Instance;
        public ILogger Logger { get; set; } = NullLogger.Instance;

        public bool IsDevelopment { get;set;}

        public IConfiguration Configuration { get;set;}

        internal TempalateOptions(Action<TempalateOptions>? action)
        {
            action?.Invoke(this);
            if (LogFactory == NullLoggerFactory.Instance && Logger != NullLogger.Instance)
                LogFactory = Logger.ToLoggerFactory();


        }

    }

 

}