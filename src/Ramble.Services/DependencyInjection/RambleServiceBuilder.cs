using Microsoft.Extensions.DependencyInjection;

namespace Ramble.Services.DependencyInjection
{
    public class RambleServiceBuilder
    {
        public IServiceCollection Services { get; }

        public RambleServiceBuilder(IServiceCollection services)
        {
            Services = services;
        }
    }
}
