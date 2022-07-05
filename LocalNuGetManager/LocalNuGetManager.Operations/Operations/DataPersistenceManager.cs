using LocalNuGetManager.Operations.Contracts.Operations;
using Microsoft.Extensions.Configuration;

namespace LocalNuGetManager.Operations.Operations
{
    public class DataPersistenceManager<TData> : IDataPersistenceManager<TData>
    {
        public TData PersistentData { get => ReadData(); set => PersistData(value); }
        
        private readonly IConfiguration _configuration;
        private readonly IJsonSerializer<TData> _jsonSerializer;

        public DataPersistenceManager(IConfiguration configuration, IJsonSerializer<TData> jsonSerializer)
        {
            _configuration = configuration;
            _jsonSerializer = jsonSerializer;
        }
        private TData ReadData()
        {
            var persistencePath = _configuration.GetSection("persistencePath").Value;
            using var streamReader = File.OpenText(persistencePath);
            return _jsonSerializer.Deserialize(streamReader);
        }

        private void PersistData(TData data)
        {
            var persistencePath = _configuration.GetSection("persistencePath").Value;
            using var streamWriter = File.CreateText(persistencePath);
        }
    }
}
