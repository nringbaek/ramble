using Ramble.Common.Core;
using System;

namespace Ramble.Common
{
    public interface IRequestContext
    {
        public RequestIdentity Identity { get; }
        public IServiceProvider ServiceProvider { get; }
    }
}
