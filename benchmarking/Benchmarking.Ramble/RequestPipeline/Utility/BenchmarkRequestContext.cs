using Ramble.Common;
using Ramble.Common.Core;
using System;

namespace Benchmarking.Ramble.RequestPipeline.Utility
{
    public class BenchmarkRequestContext : IRequestContext
    {
        public RequestIdentity Identity => new RequestIdentity
        {
            IsAuthenticated = true,
            UserId = Guid.NewGuid().ToString(),
            Roles = new[] { "User" }
        };

        public IServiceProvider ServiceProvider { get; }

        public BenchmarkRequestContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
