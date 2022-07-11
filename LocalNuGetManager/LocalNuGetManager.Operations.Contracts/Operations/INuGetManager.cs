using LocalNuGetManager.Operations.Contracts.Data;

namespace LocalNuGetManager.Operations.Contracts.Operations
{
    public interface INuGetManager
    {
        public NuGetModel NuGet { get; set; }
        public void IncreasePatch();
        public void IncreaseMinor();
        public void IncreaseMajor();
        public void Build();
        public void Publish();
    }
}
