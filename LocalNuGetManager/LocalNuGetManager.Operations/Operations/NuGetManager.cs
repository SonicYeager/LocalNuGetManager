using LocalNuGetManager.Operations.Contracts.Data;
using LocalNuGetManager.Operations.Contracts.Operations;
using System.Collections;
using System.Diagnostics;

namespace LocalNuGetManager.Operations.Operations
{
    public class NuGetManager : INuGetManager
    {
        private readonly IPathProvider _pathProvider;

        public NuGetModel NuGet { get; set; }
        
        public NuGetManager(IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
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
                });
        }
        
        public void Publish()
        {
            Debug.Assert(!string.IsNullOrEmpty(NuGet.Name), "name is not set");
            Process.Start(
                _pathProvider.GetNuGetPath(), 
                new []
                {
                    "./push",
                    "-src",
                    "http://127.0.0.1:5000/",
                    "-ApiKey",
                    "my-secret",
                    $"{_pathProvider.GetEnvironmentPath()}/{NuGet.Name}.{NuGet.Version}.nupkg",
                });
        }
    }
}
    