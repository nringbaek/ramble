using System;

namespace Ramble.Services.DependencyInjection
{
    public class RambleServiceBuilder
    {
        public RambleServiceBuilderOptions Options { get; private set; }
        public RambleServiceBuilder(RambleServiceBuilderOptions options) => Options = options;
    }
}
