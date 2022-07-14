using LocalNuGetManager.Operations.Contracts.Data;
using LocalNuGetManager.Operations.Contracts.Operations;
using LocalNuGetManager.Operations.Contracts.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Diagnostics;

namespace LocalNuGetManager.Operations.Operations
{
    public class NuGetManager : INuGetManager
    {
        private readonly IPathProvider _pathProvider;
        private readonly ILogger<NuGetManager> _logger;
        private readonly IOptionsMonitor<NuGetOptions> _options;

        public NuGetModel NuGet { get; set; }
        
        public NuGetManager(IPathProvider pathProvider, ILogger<NuGetManager> logger, IOptionsMonitor<NuGetOptions> options)
        {
            _pathProvider = pathProvider;
            _logger = logger;
            _options = options;
        }

        public void IncreasePatch()
        {
            NuGet.PatchVersion++;
        }
        
        public void IncreaseMinor()
        {
            NuGet.MinorVersion++;
        }
        
        public void IncreaseMajor()
        {
            NuGet.MajorVersion++;
        }
        
        public void Build()
        {
            Process.Start(
                _pathProvider.GetDotNetPath(), 
                new []
                {
                    "pack",
                    $"{NuGet.Name}.csproj",
                    $"-p:PackageVersion=\"{NuGet.Version}\"",
                    "--no-build",
                }).WaitForExit();
        }
        
        public void Publish()
        {
            Debug.Assert(!string.IsNullOrEmpty(NuGet.Name), "name is not set");
            Debug.Assert(!string.IsNullOrEmpty(NuGet.LastPublishPath), "path is not set");

            var path = $"{NuGet.LastPublishPath}bin\\Debug\\{NuGet.Name}.{NuGet.Version}.nupkg";
            _logger.LogInformation($"Publishing {path}");
            
            Process.Start(
                _pathProvider.GetNuGetPath(), 
                new []
                {
                    "push",
                    "-src",
                    _options.CurrentValue.ServerAddress,
                    "-ApiKey",
                    _options.CurrentValue.Secret,
                    path,
                }).WaitForExit();
        }
    }
}
    