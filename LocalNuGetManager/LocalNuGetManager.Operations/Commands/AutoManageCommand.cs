using LocalNuGetManager.Operations.Contracts.Data;
using LocalNuGetManager.Operations.Contracts.Operations;
using LocalNuGetManager.Operations.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.CommandLine;

namespace LocalNuGetManager.Operations.Commands
{
    public class AutoManageCommand : Command
    {
        private readonly INuGetManagerCollection _nuGetManagerCollection;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AutoManageCommand> _logger;
        
        public AutoManageCommand(ILogger<AutoManageCommand> logger, IServiceProvider serviceProvider, INuGetManagerCollection nuGetManagerCollection)
            : base("automanage", "Automatically manage given nuget package.")
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _nuGetManagerCollection = nuGetManagerCollection;

            var assemblyNameArg = _serviceProvider.GetService<AssemblyNameArgument>();
            var packagePathArg = _serviceProvider.GetService<PackagePathArgument>();
            
            AddArgument(assemblyNameArg!);
            AddArgument(packagePathArg!);
            this.SetHandler(AutoManage, assemblyNameArg, packagePathArg);
        }

        private void AutoManage(string assemblyName, string packagePath)
        {
            _logger.LogInformation("Read Data from CLI as: {packagePath},{assemblyName}", packagePath, assemblyName);
            
            //save/load known nugets
            _logger.LogInformation("Searches for existing NuGet Manager...");
            var nugetManager = _nuGetManagerCollection.Find(x => x.Name == assemblyName);
            if (nugetManager == null)
            {
                _logger.LogInformation("No existing NuGet Manager found. Creating new one...");
                
                nugetManager = _serviceProvider.GetService<INuGetManager>();
                var newNuGet = new NuGetModel(
                    assemblyName,
                    1,
                    0,
                    0,
                    packagePath);
                nugetManager!.NuGet = newNuGet;
                _nuGetManagerCollection.Add(nugetManager);
            }
            else
            {
                _logger.LogInformation("Existing NuGet Manager found. Updating...");
                nugetManager.NuGet.LastPublishPath = packagePath;
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
