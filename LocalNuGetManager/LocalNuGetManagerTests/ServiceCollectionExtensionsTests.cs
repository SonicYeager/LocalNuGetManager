using LocalNuGetManager.Operations.Contracts.Options;
using Microsoft.Extensions.Configuration;

namespace LocalNuGetManagerTests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void RegisterOptions_TestWithAvailableConfig_OptionFieldsAreFetched()
        {
            //var config = Substitute.For<IConfiguration>();
            //var persistenceOptions = new PersistenceOptions { Path = "SomePath" };
            //config.GetSection(Arg.Is<string>(nameof(PersistenceOptions))).Returns((CallInfo info) =>
            //{
            //    return new ConfigurationSection(persistenceOptions);
            //});
        }
    }
}