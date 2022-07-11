namespace LocalNuGetManager.Operations.Contracts.Data
{
    public class NuGetMetaData
    {
        public string RootPath { get; }
        public string AssemblyName { get; }
        
        public override string ToString()
        {
            return $"RootPath: {RootPath} | AssemblyName: {AssemblyName}";
        }
        
        public NuGetMetaData(string rootPath, string assemblyName)
        {
            RootPath = rootPath;
            AssemblyName = assemblyName;
        }
    }
}
