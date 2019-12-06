using Ramble.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ramble.Services.Authorization
{
    public class HasRoleRule : IAuthorizationRule
    {
        public string[] Roles { get; set; }

        public HasRoleRule(params string[] roles)
        {
            Roles = roles;
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
