using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ramble.Services.Authorization;
using System.Collections.Generic;

namespace Ramble.Services.DependencyInjection
{
    public class RambleServiceBuilderOptions
    {
        public IServiceCollection Services { get; set; }
        public IConfiguration Configuration { get; set; }

        public RambleServicePipelineBuilderOptions Pipeline { get; set; } = new RambleServicePipelineBuilderOptions();

        public class RambleServicePipelineBuilderOptions
        {
            public bool EnableRequestValidation { get; set; } = true;
            public bool EnableRequestAuthorization { get; set; } = true;

            public List<IAuthorizationRule> GlobalAuthorizationRules { get; set; } = new List<IAuthorizationRule>();
        }
    }
}
