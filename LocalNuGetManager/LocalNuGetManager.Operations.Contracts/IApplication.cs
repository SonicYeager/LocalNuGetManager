namespace LocalNuGetManager.Operations.Contracts
{
    public interface IApplication
    {
        public Task<int> Run();
    }
}
