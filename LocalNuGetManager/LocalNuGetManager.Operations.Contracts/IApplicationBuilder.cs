using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalNuGetManager.Operations.Contracts
{
    public interface IApplicationBuilder
    {
        public IConfigurationBuilder ConfigurationBuilder { get; }
        public IServiceCollection ServicesCollection { get; }

        public IApplication Build();
    }
}
