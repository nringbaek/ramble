using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ramble.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Ramble.Services.Repository.WallEntry
{
    public class DeleteWallEntry : Request<DeleteWallEntry>
    {
        public int EntryId { get; set; }

        public DeleteWallEntry(int entryId)
        {
            EntryId = entryId;
        }
    }

    public class DeleteWallEntryHandler : RequestHandler<DeleteWallEntry>
    {
        private readonly RambleDbContext _dbContext;

        public DeleteWallEntryHandler(RambleDbContext dbContext, ILogger<DeleteWallEntryHandler> logger) : base(logger)
        {
            _dbContext = dbContext;
        }

        public override async Task<RequestResult> Handle(DeleteWallEntry request, CancellationToken cancellationToken)
        {
            var wallEntry = await _dbContext.WallEntries.FirstOrDefaultAsync(e => e.Id == request.EntryId);
            if (wallEntry == null)
                return Error(RequestResultErrorCode.NotFound);

            // TODO: Check entry type and delete any linked resources (files etc)

            _dbContext.WallEntries.Remove(wallEntry);
            await _dbContext.SaveChangesAsync();

            return Success();
        }
    }
}
