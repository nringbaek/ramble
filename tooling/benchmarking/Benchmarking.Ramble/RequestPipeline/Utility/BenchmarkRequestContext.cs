using Ramble.Common;
using Ramble.Common.Core;
using System;
using System.Collections.Generic;

namespace Benchmarking.Ramble.RequestPipeline.Utility
{
    public class BenchmarkRequestContext : IRequestContext
    {
        public RequestIdentity Identity => RequestIdentity.Authenticated(
            userId: Guid.NewGuid().ToString(),
            roles: new List<string>() { "User" }
        );
        public IServiceProvider ServiceProvider { get; }

        public BenchmarkRequestContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
