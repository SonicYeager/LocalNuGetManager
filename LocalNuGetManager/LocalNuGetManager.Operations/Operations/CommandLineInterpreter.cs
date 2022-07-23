using LocalNuGetManager.Operations.Commands;
using LocalNuGetManager.Operations.Contracts.Operations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.CommandLine;

namespace LocalNuGetManager.Operations.Operations
{
    public class CommandLineInterpreter : ICommandLineInterpreter
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CommandLineInterpreter> _logger;
        private readonly RootCommand _rootCommand = new NuGetManagerRootCommand();

        public CommandLineInterpreter(IServiceProvider serviceProvider, ILogger<CommandLineInterpreter> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;

            SetUp();
        }

        private void SetUp()
        {
            _rootCommand.AddCommand(_serviceProvider.GetService<AutoManageCommand>()!);
        }
        
        public async Task<int> InterpretArgs(IEnumerable<string> args)
        {
            var argList = args.ToList();
            _logger.LogInformation("Invoked with args: {Join}", string.Join(" ", argList));
            return await _rootCommand.InvokeAsync(argList.ToArray());
        }
    }
}
