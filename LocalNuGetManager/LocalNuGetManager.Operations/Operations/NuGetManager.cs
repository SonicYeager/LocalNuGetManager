using LocalNuGetManager.Operations.Contracts.Data;
using LocalNuGetManager.Operations.Contracts.Operations;
using System.Collections;
using System.Diagnostics;

namespace LocalNuGetManager.Operations.Operations
{
    public class NuGetManager : INuGetManager
    {
        private readonly IPathProvider _pathProvider;
        
        public NuGetModel NuGet { get; }
        
        public NuGetManager(IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public void SetName(string name)
        {
            NuGet.Name = name;
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
            throw new NotImplementedException(); //TODO: Process.Start(_pathProvider.GetDotNetPath(), _model.Name);
        }
        
        public void Publish()
        {
            Debug.Assert(!string.IsNullOrEmpty(NuGet.Name), "name is not set");
            Process.Start(_pathProvider.GetNuGetPath(), new [] {"push", $"{NuGet.Name}.csproj", "--version", NuGet.Version}); //TODO: complete path
        }
    }
}
    