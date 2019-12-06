using Ramble.Common;
using System;

namespace Benchmarking.Ramble.RequestPipeline.Utility
{
    public class BenchmarkRequestContext : IRequestContext
    {
        private readonly IServiceProvider _serviceProvider;

        public RequestIdentity Identity => new RequestIdentity
        {
            IsAuthenticated = true,
            UserId = Guid.NewGuid().ToString(),
            Roles = new[] { "Computer" }
        };

        public IServiceProvider ServiceProvider => _serviceProvider;

        public BenchmarkRequestContext(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
