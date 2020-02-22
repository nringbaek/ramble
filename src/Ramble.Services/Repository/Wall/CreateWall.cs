using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ramble.Data;
using Ramble.Data.Models;

namespace Ramble.Services.Repository.Wall
{
    public class CreateWall : Request<CreateWall, int>
    {
        public string Name { get; set; } = null!;

        public class Validator : RequestValidator<CreateWall>
        {
            public Validator()
            {
                RuleFor(e => e.Name).NotEmpty();
            }
        }
    }

    public class CreateWallHandler : RequestHandler<CreateWall, int>
    {
        private readonly RambleDbContext _dbContext;

        public CreateWallHandler(RambleDbContext dbContext, ILogger<CreateWallHandler> logger) : base(logger)
        {
            _dbContext = dbContext;
        }

        public override async Task<RequestResult<int>> Handle(CreateWall request, CancellationToken cancellationToken)
        {
            var hasExistingWall = await _dbContext.Walls.AnyAsync(e => e.Name == request.Name);
            if (hasExistingWall)
                return Error(RequestResultErrorCode.BadRequest);

            var wall = new WallEntity
            {
                Name = request.Name
            };

            _dbContext.Walls.Add(wall);
            await _dbContext.SaveChangesAsync();

            return Success(wall.Id);
        }
    }
}
