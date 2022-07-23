using LocalNuGetManager.Operations.Contracts;
using LocalNuGetManager.Operations.Contracts.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LocalNuGetManager.Operations
{
    public class Application : IApplication
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly ILogger<Application> _logger;

        public Application(IServiceProvider serviceProvider, IConfiguration configuration, ILogger<Application> logger)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<int> Run()
        {
            try
            {
                var cliInterpreter = _serviceProvider.GetService<ICommandLineInterpreter>();
                var args = Environment.GetCommandLineArgs().ToList();
                args.RemoveAt(0);
                return await cliInterpreter!.InterpretArgs(args);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "An error occurred while running the application.");
                throw;
            }
        }
        
        public static IApplicationBuilder CreateBuilder()
        {
            return new ApplicationBuilder();
        }
    }
}
