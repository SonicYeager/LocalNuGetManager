using System.Text;
using LocalNuGetManager.Operations.Contracts;
using LocalNuGetManager.Operations.Contracts.Operations;
using LocalNuGetManager.Operations.Extensions;
using LocalNuGetManager.Operations.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace LocalNuGetManager.Operations
{
    public class ApplicationBuilder : IApplicationBuilder
    {
        public IConfigurationBuilder ConfigurationBuilder { get; }
        public IConfiguration Configuration { get; }
        public IServiceCollection ServicesCollection { get; }
        public IServiceProvider ServicesProvider { get; }

        public ApplicationBuilder()
        {
            ServicesCollection = new ServiceCollection();
            ConfigurationBuilder = new ConfigurationBuilder();
            
            //Configure Standards
            ConfigurationBuilder
                .AddJsonFile("appsettings.json");
            
            Configuration = ConfigurationBuilder.Build();

            //Register Standards
            ServicesCollection.AddSingleton(Configuration);
            ServicesCollection.RegisterOptions(Configuration);
            ServicesCollection.RegisterOperations();
            ServicesCollection.AddLogging(o => o.AddConsole());
            ServicesCollection.AddTransient<IApplication, Application>();
            
            ServicesProvider = ServicesCollection.BuildServiceProvider();
        }
        
        public IApplication Build()
        {
            return ServicesProvider.GetService<IApplication>();
        }
    }
}
