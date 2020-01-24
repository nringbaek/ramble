using Microsoft.EntityFrameworkCore;
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

        public override async Task<bool> IsAuthorized(CanAdministerWallRule rule)
        {
            var wall = await _dbContext.Walls.FirstOrDefaultAsync(e => e.Id == rule.WallId);
            return wall != null && wall.CreatorId == _requestContext.Identity.UserId;
        }
    }
}
