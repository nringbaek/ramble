using Ramble.Common;
using Ramble.Data;
using Ramble.Services.Authorization;
using System.Threading.Tasks;

namespace Ramble.Services.Repository.Wall.Rules
{
    public class CanAdministerWallRule : IAuthorizationRule
    {
        public int WallId { get; set; }
    }

    public class CanAdministerWallRuleEngine : AuthorizationRuleEngine<CanAdministerWallRule>
    {
        private readonly RambleDbContext _dbContext;
        private readonly IRequestContext _requestContext;

        public CanAdministerWallRuleEngine(RambleDbContext dbContext, IRequestContext requestContext)
        {
            _dbContext = dbContext;
            _requestContext = requestContext;
        }

        public override Task<bool> IsAuthorized(CanAdministerWallRule rule)
        {
            return Task.FromResult(true);
        }
    }
}
