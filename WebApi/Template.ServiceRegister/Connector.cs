
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;


using Template.ServiceRegister.ServiceRegister;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Template.Application;
using Template.Infrastructure;
using Microsoft.Extensions.Options;


namespace Template.ServiceRegister.Connector
{
    public class Connector
    {
        private TempalateOptions Options { get; }

        private ILogger Logger { get; }
        public bool isDevelopment { get; set; }

        public IConfiguration Configuration { get; set; }
     

        private Connector(TempalateOptions options)
        {
            Options = options;
            Configuration = options.Configuration;
            isDevelopment = options.IsDevelopment;
            Logger = options.LogFactory.CreateLogger<Connector>();
            AssemblyName name = Assembly.GetExecutingAssembly().GetName();
            Logger.LogInformation("{Name} v{Version}.", name.Name, name.Version);
        }
        public static async Task<ITemplateClient> ConnectAsync(Action<TempalateOptions>? action = null,CancellationToken ct = default)
        {
            TempalateOptions options = new(action);
            Connector connector = new(options);
            ITemplateClient client = await connector.RegisterAsync(ct).ConfigureAwait(false); 
            return client;
        }

        private async Task<ITemplateClient> RegisterAsync(CancellationToken ct)
        {
          
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
  
            try
            {
             
                return new ServiceCollection()
                    .AddSingleton(Options)
                    .AddSingleton(Options.LogFactory)
                    .AddSingleton(Logger)
                    .AddSingleton(Configuration)
                    .AddLogging()
                    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()))
                    .AddHttpContextAccessor()
                    .AddApplication(Configuration)
                    .AddInfrastructure(Configuration)
                
                    .BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true })
                    .GetRequiredService<ITemplateClient>();
            }
            catch (Exception)
            {
               
                throw;
            }
        }

      

    }
}
