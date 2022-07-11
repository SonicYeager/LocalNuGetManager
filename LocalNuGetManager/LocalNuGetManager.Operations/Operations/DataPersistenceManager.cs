using LocalNuGetManager.Operations.Contracts.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LocalNuGetManager.Operations.Operations
{
    public class DataPersistenceManager<TData> : IDataPersistenceManager<TData>
    {
        public TData PersistentData { get => ReadData(); set => PersistData(value); }
        
        private readonly IConfiguration _configuration;
        private readonly IPathProvider _pathProvider;
        private readonly IJsonSerializer<TData> _jsonSerializer;
        private readonly ILogger<DataPersistenceManager<TData>> _logger;

        public DataPersistenceManager(IConfiguration configuration, IJsonSerializer<TData> jsonSerializer, ILogger<DataPersistenceManager<TData>> logger, IPathProvider pathProvider)
        {
            _configuration = configuration;
            _jsonSerializer = jsonSerializer;
            _logger = logger;
            _pathProvider = pathProvider;
        }
        
        private TData ReadData()
        {
            try
            {
                var persistencePath = $"{_pathProvider.GetEnvironmentPath()}\\{_configuration.GetSection("persistencePath").Value}";
                using var streamReader = File.OpenText(persistencePath);
                return _jsonSerializer.Deserialize(streamReader);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to read data from {Env}\\{Path}", _pathProvider.GetEnvironmentPath(), _configuration.GetSection("persistencePath").Value);
                throw;
            }
        }

        private void PersistData(TData data)
        {
            var persistencePath = $"{_pathProvider.GetEnvironmentPath()}\\{_configuration.GetSection("persistencePath").Value}";
            using var streamWriter = File.CreateText(persistencePath);
            _jsonSerializer.Serialize(streamWriter, data);
        }
    }
}
