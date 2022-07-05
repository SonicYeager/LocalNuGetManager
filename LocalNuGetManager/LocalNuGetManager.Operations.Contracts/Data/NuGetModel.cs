namespace LocalNuGetManager.Operations.Contracts.Data
{
    public class NuGetModel
    {
        public string Name { get; }
        public int MajorVersion { get; }
        public int MinorVersion { get; }
        public int PatchVersion { get; }
        public string Version { get; }

        public NuGetModel(string name, int majorVersion, int minorVersion, int patchVersion, string version)
        {
            Name = name;
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
            PatchVersion = patchVersion;
            Version = version;
        }
    }
}
