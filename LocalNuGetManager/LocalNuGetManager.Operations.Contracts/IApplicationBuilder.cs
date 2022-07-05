using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalNuGetManager.Operations.Contracts
{
    public interface IApplicationBuilder
    {
        public IConfigurationBuilder Configuration { get; }
        public IServiceCollection Services { get; }

        public IApplication Build();
    }
}
