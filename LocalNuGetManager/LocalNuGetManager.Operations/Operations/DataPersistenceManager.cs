using LocalNuGetManager.Operations.Contracts.Operations;
using LocalNuGetManager.Operations.Contracts.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LocalNuGetManager.Operations.Operations
{
    public class DataPersistenceManager<TData> : IDataPersistenceManager<TData>
    {
        public TData PersistentData { get => ReadData(); set => PersistData(value); }
        
        private readonly IPathProvider _pathProvider;
        private readonly IJsonSerializer<TData> _jsonSerializer;
        private readonly ILogger<DataPersistenceManager<TData>> _logger;
        private readonly IOptionsMonitor<PersistenceOptions> _options;

        public DataPersistenceManager(IJsonSerializer<TData> jsonSerializer, ILogger<DataPersistenceManager<TData>> logger, IPathProvider pathProvider, IOptionsMonitor<PersistenceOptions> options)
        {
            _jsonSerializer = jsonSerializer;
            _logger = logger;
            _pathProvider = pathProvider;
            _options = options;
        }
        
        private TData ReadData()
        {
            try
            {
                var persistencePath = $"{_pathProvider.GetEnvironmentPath()}\\{_options.CurrentValue.Path}";
                using var streamReader = File.OpenText(persistencePath);
                return _jsonSerializer.Deserialize(streamReader);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to read data from {Env}\\{Path}", _pathProvider.GetEnvironmentPath(), _options.CurrentValue.Path);
                throw;
            }
        }

        private void PersistData(TData data)
        {
            var persistencePath = $"{_pathProvider.GetEnvironmentPath()}\\{_options.CurrentValue.Path}";
            using var streamWriter = File.CreateText(persistencePath);
            _jsonSerializer.Serialize(streamWriter, data);
        }
    }
}
