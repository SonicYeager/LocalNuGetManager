using LocalNuGetManager.Operations.Contracts.Operations;
using Microsoft.Extensions.Logging;

namespace LocalNuGetManager.Operations.Operations
{
    public class PathProvider : IPathProvider
    {
        private readonly string _enviromentPath = Environment.GetEnvironmentVariable("PATH");
        private readonly ILogger<PathProvider> _logger;
        
        public PathProvider(ILogger<PathProvider> logger)
        {
            _logger = logger;
        }

        public string GetEnvironmentPath()
        { 
           var file = System.Reflection.Assembly.GetEntryAssembly()!.Location;
           return Path.GetDirectoryName(file);
        }
        public string GetDotNetPath()
        {
            return GetExecutablePath("dotnet.exe");
        }
        public string GetNuGetPath()
        {
            return GetExecutablePath("nuget.exe");
        }
        
        private string GetExecutablePath(string executable)
        {
            var paths = _enviromentPath.Split(';');
            var exePath =  paths.Select(x => Path.Combine(x, executable))
                .Where(File.Exists)
                .FirstOrDefault();
            _logger.LogInformation("Found {0} at {1}", executable, exePath);
            return exePath;
        }
    }
}
