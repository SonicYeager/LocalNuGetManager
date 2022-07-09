using LocalNuGetManager.Operations.Contracts.Operations;

namespace LocalNuGetManager.Operations.Operations
{
    public class PathProvider : IPathProvider
    {
        public string GetEnvironmentPath()
        {
           return Environment.CurrentDirectory;
        }
        public string GetDotNetPath()
        {
            return "dotnet";
        }
        public string GetNuGetPath()
        {
            return "nuget";
        }
    }
}
