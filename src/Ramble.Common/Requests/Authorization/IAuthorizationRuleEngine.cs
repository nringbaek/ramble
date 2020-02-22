using System;
using System.Threading.Tasks;

namespace Ramble.Common.Requests.Authorization
{
    public interface IAuthorizationRuleEngine
    {
        Task<bool> IsAuthorized(IAuthorizationRule rule);
    }

    public abstract class AuthorizationRuleEngine<TRule> : IAuthorizationRuleEngine where TRule : IAuthorizationRule
    {
        protected static readonly Task<bool> AuthorizedTask = Task.FromResult(true);
        protected static readonly Task<bool> UnauthorizedTask = Task.FromResult(false);

        public abstract Task<bool> IsAuthorized(TRule rule);
        Task<bool> IAuthorizationRuleEngine.IsAuthorized(IAuthorizationRule rule) => IsAuthorized((TRule) rule);
    }
}
