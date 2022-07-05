namespace LocalNuGetManager.Operations.Contracts.Operations
{
    public interface IJsonSerializer<TObject>
    {
        public TObject Deserialize(TextReader reader);
        public void Serialize(TextWriter writer, TObject obj);
    }
}
