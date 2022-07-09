using LocalNuGetManager.Operations.Contracts;
using LocalNuGetManager.Operations.Contracts.Operations;
using LocalNuGetManager.Operations.Extensions;
using LocalNuGetManager.Operations.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalNuGetManager.Operations
{
    public class ApplicationBuilder : IApplicationBuilder
    {

        public IConfigurationBuilder Configuration { get; }
        public IServiceCollection Services { get; }

        public ApplicationBuilder()
        {
            Services = new ServiceCollection();
            Configuration = new ConfigurationBuilder();
            
            //Configure Standards
            Configuration
                .AddJsonFile("appsettings.json");
            
            //Register Standards
            Services.RegisterOperations();
        }
        
        public IApplication Build()
        {
            return new Application(Services.BuildServiceProvider(), Configuration.Build());
        }
    }
}
