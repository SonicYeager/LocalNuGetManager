using LocalNuGetManager.Operations.Contracts.Data;
using LocalNuGetManager.Operations.Contracts.Operations;
using Microsoft.Extensions.Logging;
using System.Collections;
using Microsoft.Extensions.DependencyInjection;

namespace LocalNuGetManager.Operations.Operations
{
    public class NuGetManagerCollection : INuGetManagerCollection
    {
        private readonly IDataPersistenceManager<ICollection<NuGetModel>> _dataPersistenceManager;
        private readonly ICollection<INuGetManager> _nugetManagers;
        private readonly ILogger<NuGetManagerCollection> _logger;
        private readonly IServiceProvider _serviceProvider;

        public int Count { get => _nugetManagers.Count; }
        public bool IsReadOnly { get => _nugetManagers.IsReadOnly; }

        public NuGetManagerCollection(IDataPersistenceManager<ICollection<NuGetModel>> dataPersistenceManager, ILogger<NuGetManagerCollection> logger, IServiceProvider serviceProvider)
        {
            _dataPersistenceManager = dataPersistenceManager;
            _logger = logger;
            _serviceProvider = serviceProvider;

            try
            {
                _nugetManagers = _dataPersistenceManager.PersistentData.Select(n =>
                {
                    var manager = _serviceProvider.GetService<INuGetManager>();
                    manager!.NuGet = n;
                    return manager;
                }).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to load nuget managers, created empty collection!");
                _nugetManagers = new List<INuGetManager>();
            }
        }
        
        public INuGetManager Find(Predicate<NuGetModel> predicate)
        {
            return _nugetManagers.FirstOrDefault(nugetManager => predicate(nugetManager.NuGet));
        }
        
        public void Save()
        {
            _dataPersistenceManager.PersistentData = _nugetManagers.Select(m => m.NuGet).ToList();
        }
        
        public IEnumerator<INuGetManager> GetEnumerator()
        {
            return _nugetManagers.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public void Add(INuGetManager item)
        {
            _nugetManagers.Add(item);
        }
        
        public void Clear()
        {
            throw new InvalidOperationException("Collection cannot be cleared. Do it manually at the persistence file location.");
        }
        
        public bool Contains(INuGetManager item)
        {
            return _nugetManagers.Contains(item);
        }
        
        public void CopyTo(INuGetManager[] array, int arrayIndex)
        {
            _nugetManagers.CopyTo(array, arrayIndex);
        }
        
        public bool Remove(INuGetManager item)
        {
            return _nugetManagers.Remove(item);
        }
    }
}
