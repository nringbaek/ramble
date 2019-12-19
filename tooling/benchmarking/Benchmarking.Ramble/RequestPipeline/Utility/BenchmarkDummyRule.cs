using Ramble.Common;
using Ramble.Services.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Benchmarking.Ramble.RequestPipeline.Utility
{
    public class BenchmarkDummyRule : IAuthorizationRule
    {
        public bool HasAnswerToTheQuestion { get; set; }

        public class RuleEngine : AuthorizationRuleEngine<BenchmarkDummyRule>
        {
            private readonly IRequestContext _requestContext;

            public RuleEngine(IRequestContext requestContext)
            {
                _requestContext = requestContext;
            }

            public override Task<bool> IsAuthorized(BenchmarkDummyRule rule)
            {
                return Task.FromResult(
                    _requestContext.Identity.Roles.Contains("Computer") &&
                    rule.HasAnswerToTheQuestion
                );
            }
        }
    }
}
