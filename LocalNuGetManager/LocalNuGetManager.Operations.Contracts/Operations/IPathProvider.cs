namespace LocalNuGetManager.Operations.Contracts.Operations
{
    public interface IPathProvider
    {
        public string GetEnvironmentPath();
        public string GetDotNetPath();
        public string GetNuGetPath();
    }
}
