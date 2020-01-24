using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ramble.Data;
using Ramble.Services.Repository.Wall.Rules;

namespace Ramble.Services.Repository.Wall
{
    public class DeleteWall : Request<DeleteWall>
    {
        public int WallId { get; }

        public DeleteWall(int wallId)
        {
            WallId = wallId;
        }

        public class Validator : RequestValidator<DeleteWall>
        {
            public Validator()
            {
                RuleFor(e => e.WallId).GreaterThan(0);
            }
        }

        public class Authorizer : RequestAuthorizer<DeleteWall>
        {
            public Authorizer()
            {
                AddRule(wall => new CanAdministerWallRule
                {
                    WallId = wall.WallId
                });
            }
        }
    }

    public class DeleteWallHandler : RequestHandler<DeleteWall>
    {
        private readonly RambleDbContext _dbContext;

        public DeleteWallHandler(RambleDbContext dbContext, ILogger<DeleteWallHandler> logger) : base(logger)
        {
            _dbContext = dbContext;
        }

        public override async Task<RequestResult> Handle(DeleteWall request, CancellationToken cancellationToken)
        {
            var wall = await _dbContext.Walls.FirstOrDefaultAsync(e => e.Id == request.WallId);
            if (wall == null)
                return Error(RequestResultErrorCode.NotFound);

            _dbContext.Walls.Remove(wall);
            await _dbContext.SaveChangesAsync();

            return Success();
        }
    }
}