using LocalNuGetManager.Operations.Contracts.Operations;
using LocalNuGetManager.Operations.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace LocalNuGetManager.Operations.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterOperations(this IServiceCollection services)
        {
            services.AddTransient<ICommandLineInterpreter, CommandLineInterpreter>();
            services.AddTransient(typeof(IDataPersistenceManager<>), typeof(DataPersistenceManager<>));
            services.AddTransient<IDataPersistenceManager<ICollection<INuGetManager>>, DataPersistenceManager<ICollection<INuGetManager>>>();
            services.AddTransient(typeof(IJsonSerializer<>), typeof(NewtonsoftJsonSerializer<>));
            services.AddTransient<IJsonSerializer<ICollection<INuGetManager>>, NewtonsoftJsonSerializer<ICollection<INuGetManager>>>();
            services.AddTransient<INuGetManager, NuGetManager>();
            services.AddTransient<INuGetManagerCollection, NuGetManagerCollection>();
            services.AddTransient<IPathProvider, PathProvider>();
        }
    }
}
