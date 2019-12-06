using FluentValidation;
using Microsoft.Extensions.Logging;
using Ramble.Data;
using Ramble.Data.Models;
using Ramble.Data.Models.Types;
using Ramble.Services.Repository.Wall.Rules;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ramble.Services.Repository.WallEntry
{
    public class CreateWallEntry : Request<CreateWallEntry, int>
    {
        public int WallId { get; set; }
        public EntryType EntryType { get; set; }
        public string EntryContent { get; set; }
        public DateTime EntryTimestamp { get; set; }

        public class Validator : RequestValidator<CreateWallEntry>
        {
            public Validator()
            {
                RuleFor(e => e.EntryContent).NotEmpty();
            }
        }

        public class Authorizer : RequestAuthorizer<CreateWallEntry>
        {
            public Authorizer()
            {
                AddRule(entry => new CanAdministerWallRule
                {
                    WallId = entry.WallId
                });
            }
        }
    }

    public class CreateWallEntryHandler : RequestHandler<CreateWallEntry, int>
    {
        private readonly RambleDbContext _dbContext;

        public CreateWallEntryHandler(RambleDbContext dbContext, ILogger<CreateWallEntryHandler> logger) : base(logger)
        {
            _dbContext = dbContext;
        }

        public override async Task<RequestResult<int>> Handle(CreateWallEntry request, CancellationToken cancellationToken)
        {
            var wallEntry = new WallEntryEntity
            {
                WallId = request.WallId,
                EntryType = request.EntryType,
                EntryContent = request.EntryContent,
                EntryTimestamp = request.EntryTimestamp,

                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now
            };

            _dbContext.WallEntries.Add(wallEntry);
            await _dbContext.SaveChangesAsync();

            return Success(wallEntry.Id);
        }
    }
}
