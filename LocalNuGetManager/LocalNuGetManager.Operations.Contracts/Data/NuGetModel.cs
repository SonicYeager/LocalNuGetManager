namespace LocalNuGetManager.Operations.Contracts.Data
{
    public class NuGetModel
    {
        public string Name { get; set; } = string.Empty;
        public int MajorVersion { get; set; } = 1;
        public int MinorVersion { get; set; } = 0;
        public int PatchVersion { get; set; } = 0;
        public string Version { get => $"{MajorVersion}.{MinorVersion}.{PatchVersion}"; }
        public string LastPublishPath { get; set; }

        public NuGetModel(string name, int majorVersion, int minorVersion, int patchVersion, string lastPublishPath)
        {
            Name = name;
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
            PatchVersion = patchVersion;
            LastPublishPath = lastPublishPath;
        }
    }
}
