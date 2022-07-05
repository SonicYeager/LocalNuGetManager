namespace LocalNuGetManager.Operations.Contracts.Operations
{
    public interface IDataPersistenceManager<TData>
    {
        public TData PersistentData { get; set; }
    }
}
