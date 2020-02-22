using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ramble.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ramble.Services.Repository.WallEntry
{
    public class UpdateWallEntry : Request<UpdateWallEntry>
    {
        public int WallEntryId { get; set; }

        public string EntryContent { get; set; } = null!;
        public DateTime? EntryTimestamp { get; set; }

        public class Validator : RequestValidator<UpdateWallEntry>
        {
            public Validator()
            {
                RuleFor(e => e).Must(e =>
                    e.EntryContent != null ||
                    e.EntryTimestamp != null
                );
            }
        }
    }

    public class UpdateWallEntryHandler : RequestHandler<UpdateWallEntry>
    {
        private readonly RambleDbContext _dbContext;

        public UpdateWallEntryHandler(RambleDbContext dbContext, ILogger<RequestHandler<UpdateWallEntry>> logger) : base(logger)
        {
            _dbContext = dbContext;
        }

        public override async Task<RequestResult> Handle(UpdateWallEntry request, CancellationToken cancellationToken)
        {
            var entry = await _dbContext.WallEntries
                .AsTracking()
                .FirstOrDefaultAsync(e => e.Id == request.WallEntryId);

            if (entry == null)
                return Error(RequestResultErrorCode.NotFound);

            if (request.EntryContent != null)
                entry.EntryContent = request.EntryContent;

            if (request.EntryTimestamp != null)
                entry.EntryTimestamp = request.EntryTimestamp.Value;

            await _dbContext.SaveChangesAsync();
            return Success();
        }
    }
}
