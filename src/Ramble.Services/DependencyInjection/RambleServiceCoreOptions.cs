using Ramble.Common.Requests.Authorization;
using System.Collections.Generic;

namespace Ramble.Services.DependencyInjection
{
    public class RambleServiceCoreOptions
    {
        public RambleServicePipelineBuilderOptions Pipeline { get; } = new RambleServicePipelineBuilderOptions();

        public class RambleServicePipelineBuilderOptions
        {
            public bool EnableRequestValidation { get; set; } = true;
            public bool EnableRequestAuthorization { get; set; } = true;

            public List<IAuthorizationRule> GlobalAuthorizationRules { get; } = new List<IAuthorizationRule>();
        }
    }
}
