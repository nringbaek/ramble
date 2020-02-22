using Ramble.Common.Requests.Authorization;
using System.Threading.Tasks;

namespace Ramble.Services.Authorization.Rules
{
    public class IsAuthenticatedRule : IAuthorizationRule
    {
        public class IsAuthenticatedRuleEngine : AuthorizationRuleEngine<IsAuthenticatedRule>
        {
            private readonly IRequestContext _requestContext;

            public IsAuthenticatedRuleEngine(IRequestContext requestContext)
            {
                _requestContext = requestContext;
            }

            public override Task<bool> IsAuthorized(IsAuthenticatedRule rule)
            {
                return _requestContext.Identity.IsAuthenticated
                    ? AuthorizedTask
                    : UnauthorizedTask;
            }
        }
    }
}
