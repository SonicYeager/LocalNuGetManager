using LocalNuGetManager.Operations.Contracts;
using LocalNuGetManager.Operations.Contracts.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalNuGetManager.Operations
{
    public class Application : IApplication
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public Application(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public void Run()
        {
            try
            {
                var cliInterpreter = _serviceProvider.GetService<ICommandLineInterpreter>();
                var args = Environment.GetCommandLineArgs();
                cliInterpreter!.InterpretArgs(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public static IApplicationBuilder CreateBuilder()
        {
            return new ApplicationBuilder();
        }
    }
}
