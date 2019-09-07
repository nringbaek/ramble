using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ramble.Services.DependencyInjection;

namespace Ramble.Services
{
    public static class DependencyBuilder
    {
        public static RambleServiceBuilder AddHomeServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<ElasticSearchOptions>(options => configuration.GetSection("ElasticSearch").Bind(options));

            //services.AddSingleton<IFileContext, HomeFileContext>();
            //services.AddSingleton<ISearchContext, HomeSearchContext>();

            //services.AddSingleton<ISocketActionOrchestration, SocketActionOrchestration>();
            //services.AddSingleton<ISocketActionManager, TestSocetActionManager>();

            return new RambleServiceBuilder(services, configuration);
        }
    }
}
