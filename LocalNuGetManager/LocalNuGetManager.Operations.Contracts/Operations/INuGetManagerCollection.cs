using LocalNuGetManager.Operations.Contracts.Data;
using System.Linq.Expressions;

namespace LocalNuGetManager.Operations.Contracts.Operations
{
    public interface INuGetManagerCollection : ICollection<INuGetManager>
    {
        public INuGetManager Find(Predicate<NuGetModel> expression);
        public void Save();
    }
}
