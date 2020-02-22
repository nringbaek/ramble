using System;

namespace Ramble
{
    public interface IRequestContext
    {
        public RequestIdentity Identity { get; }
        public IServiceProvider ServiceProvider { get; }
    }
}
