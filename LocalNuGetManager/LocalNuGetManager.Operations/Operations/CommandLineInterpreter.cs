using LocalNuGetManager.Operations.Contracts.Data;
using LocalNuGetManager.Operations.Contracts.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace LocalNuGetManager.Operations.Operations
{
    public class CommandLineInterpreter : ICommandLineInterpreter
    {
        private readonly INuGetManagerCollection _nuGetManagerCollection;
        private readonly IPathProvider _pathProvider;
        private readonly IServiceProvider _serviceProvider;
        
        public CommandLineInterpreter(INuGetManagerCollection nuGetManagerCollection, IPathProvider pathProvider, IServiceProvider serviceProvider)
        {
            _nuGetManagerCollection = nuGetManagerCollection;
            _pathProvider = pathProvider;
            _serviceProvider = serviceProvider;
        }
        
        public void InterpretArgs(IEnumerable<string> args)
        {
            //read path (seperate into rootPath/AssemblyName)
            var metaData = new NuGetMetaData(_pathProvider.GetEnvironmentPath(),args.ToList()[1]);
            
            //save/load known nugets
            var nugetManager = _nuGetManagerCollection.Find(x => x.Name == metaData.AssemblyName);
            if (nugetManager == null)
            {
                nugetManager = _serviceProvider.GetService<INuGetManager>();
                nugetManager!.SetName(metaData.AssemblyName);
                _nuGetManagerCollection.Add(nugetManager);
            }
            
            //increase version
            nugetManager.IncreasePatch();
            
            //save on known nugets
            _nuGetManagerCollection.Save();
            
            //run cmds 
            nugetManager.Build();
            nugetManager.Publish();
        }
    }
}
