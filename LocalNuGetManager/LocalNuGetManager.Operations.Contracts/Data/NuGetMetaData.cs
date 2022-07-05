namespace LocalNuGetManager.Operations.Contracts.Data
{
    public class NuGetMetaData
    {
        public string RootPath { get; }
        public string AssemblyName { get; }
        
        public NuGetMetaData(string rootPath, string assemblyName)
        {
            RootPath = rootPath;
            AssemblyName = assemblyName;
        }
    }
}
