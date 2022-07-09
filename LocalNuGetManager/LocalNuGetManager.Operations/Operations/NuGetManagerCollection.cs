using LocalNuGetManager.Operations.Contracts.Data;
using LocalNuGetManager.Operations.Contracts.Operations;
using System.Collections;

namespace LocalNuGetManager.Operations.Operations
{
    public class NuGetManagerCollection : INuGetManagerCollection
    {
        private readonly IDataPersistenceManager<ICollection<INuGetManager>> _dataPersistenceManager;
        private readonly ICollection<INuGetManager> _nugetManagers;

        public int Count { get => _nugetManagers.Count; }
        public bool IsReadOnly { get => _nugetManagers.IsReadOnly; }

        public NuGetManagerCollection(IDataPersistenceManager<ICollection<INuGetManager>> dataPersistenceManager)
        {
            _dataPersistenceManager = dataPersistenceManager;
            _nugetManagers = _dataPersistenceManager.PersistentData;
        }
        
        public INuGetManager Find(Predicate<NuGetModel> predicate)
        {
            return _nugetManagers.FirstOrDefault(nugetManager => predicate(nugetManager.NuGet));
        }
        
        public void Save()
        {
            _dataPersistenceManager.PersistentData = _nugetManagers;
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
