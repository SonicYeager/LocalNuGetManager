using LocalNuGetManager.Operations.Contracts.Data;
using LocalNuGetManager.Operations.Contracts.Operations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LocalNuGetManager.Operations.Operations
{
    public class CommandLineInterpreter : ICommandLineInterpreter
    {
        private readonly INuGetManagerCollection _nuGetManagerCollection;
        private readonly IPathProvider _pathProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CommandLineInterpreter> _logger;

        public CommandLineInterpreter(INuGetManagerCollection nuGetManagerCollection, IPathProvider pathProvider, IServiceProvider serviceProvider, ILogger<CommandLineInterpreter> logger)
        {
            _nuGetManagerCollection = nuGetManagerCollection;
            _pathProvider = pathProvider;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        
        public void InterpretArgs(IEnumerable<string> args)
        {
            //read path (seperate into rootPath/AssemblyName)
            var metaData = new NuGetMetaData(args.ToList()[2],args.ToList()[1]);
            _logger.LogInformation("Read Data from CLI as: {MetaData}", metaData);
            
            //save/load known nugets
            _logger.LogInformation("Searches for existing NuGet Manager...");
            var nugetManager = _nuGetManagerCollection.Find(x => x.Name == metaData.AssemblyName);
            if (nugetManager == null)
            {
                _logger.LogInformation("No existing NuGet Manager found. Creating new one...");
                
                nugetManager = _serviceProvider.GetService<INuGetManager>();
                var newNuGet = new NuGetModel(
                    metaData.AssemblyName,
                    1,
                    0,
                    0,
                    metaData.RootPath);
                nugetManager!.NuGet = newNuGet;
                _nuGetManagerCollection.Add(nugetManager);
            }
            else
            {
                _logger.LogInformation("Existing NuGet Manager found. Updating...");
                nugetManager.NuGet.LastPublishPath = metaData.RootPath;
            }

            //increase version
            _logger.LogInformation("Increasing version from {nugetManager.NuGet.Version}...", nugetManager.NuGet.Version);
            nugetManager.IncreasePatch();
            _logger.LogInformation("to {nugetManager.NuGet.Version}", nugetManager.NuGet.Version);
            
            //save on known nugets
            _logger.LogInformation("Saving NuGet Manager...");
            _nuGetManagerCollection.Save();
            
            //run cmds 
            _logger.LogInformation("Running commands...");
            nugetManager.Build();
            nugetManager.Publish();
        }
    }
}
