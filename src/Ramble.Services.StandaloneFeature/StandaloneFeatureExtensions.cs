using Ramble.Services.DependencyInjection;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StandaloneFeatureExtensions
    {
        public static RambleServiceBuilder AddStandaloneFeature(this RambleServiceBuilder builder)
        {
            // Register services etc needed for this feature
            return builder;
        }

    }
}
