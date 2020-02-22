using Ramble.Common.Requests.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ramble.Services.Authorization
{
    public class HasRoleRule : IAuthorizationRule
    {
        public List<string> Roles { get; }

        public HasRoleRule(params string[] roles)
        {
            Roles = roles.ToList();
        }

        public class RequiresRoleEngine : AuthorizationRuleEngine<HasRoleRule>
        {
            private readonly IRequestContext _requestContext;

            public RequiresRoleEngine(IRequestContext requestContext)
            {
                _requestContext = requestContext;
            }

            public override Task<bool> IsAuthorized(HasRoleRule rule)
            {
                if (!_requestContext.Identity.IsAuthenticated)
                    return UnauthorizedTask;

                foreach (var role in rule.Roles)
                {
                    if (!_requestContext.Identity.Roles.Contains(role))
                        return UnauthorizedTask;
                }

                return AuthorizedTask;
            }
        }
    }
}
