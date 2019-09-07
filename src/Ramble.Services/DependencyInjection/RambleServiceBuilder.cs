using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ramble.Services.DependencyInjection
{
    public class RambleServiceBuilder
    {
        public IServiceCollection Services { get; }
        public IConfiguration Configuration { get; }

        public RambleServiceBuilder(IServiceCollection services, IConfiguration configuration)
        {
            Services = services;
            Configuration = configuration;
        }
    }
}
