using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application;
using Template.Infrastructure.Loggers;
using Template.ServiceRegister.Connector;
using Template.ServiceRegister.ServiceRegister;
using Xunit.Abstractions;

namespace UnitTesting.Base
{

    public abstract class UnitTestBase : IDisposable
    {
        protected ITemplateClient templateClient;
        protected UnitTestBase()
        {

            var logPath = Path.Combine("D:\\Projects\\.NET Core\\Stock Market\\Berza\\Berza\\Berza.ConsoleApp", "Logs", "log.txt");




            var serlogger = new LoggerConfiguration()
                .WriteTo.File(logPath,
                              rollingInterval: RollingInterval.Infinite,
                              outputTemplate: "{Timestamp:MM/dd/yyyy H:mm:ss zzzz} {ThreadId} {Level} {SourceContext} {Message:lj}{NewLine}{Exception}")
                .CreateLogger();





            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Test.json", optional: true, reloadOnChange: true)
                .Build();


            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder
            .AddProvider(new ConsoleLoggerProvider(LogLevel.Information, ConsoleColor.Red))
            .AddFilter("Microsoft.EntityFrameworkCore", LogLevel.None)
            //.AddSimpleConsole(c => c.SingleLine = true)
            .AddSerilog(serlogger)
            .SetMinimumLevel(LogLevel.Trace));


            templateClient =  Connector.ConnectAsync(options => {
                options.LogFactory = loggerFactory;
                options.IsDevelopment = true;
                options.Configuration = configuration;

            }).ConfigureAwait(false).GetAwaiter().GetResult();



          //  templateClient =  Connector.ConnectAsync(options).GetAwaiter().GetResult(); 

         
        

        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

}
