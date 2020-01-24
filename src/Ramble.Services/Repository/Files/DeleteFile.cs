using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ramble.Common;
using Ramble.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Ramble.Services.Repository.Files
{
    public class DeleteFile : Request<DeleteFile>
    {
        public int Id { get; }

        public DeleteFile(int id)
        {
            Id = id;
        }
    }

    public class DeleteFileHandler : RequestHandler<DeleteFile>
    {
        private readonly IFileStorage _fileStorage;
        private readonly RambleDbContext _dbContext;

        public DeleteFileHandler(IFileStorage fileStorage, RambleDbContext dbContext, ILogger<DeleteFileHandler> logger) : base(logger)
        {
            _fileStorage = fileStorage;
            _dbContext = dbContext;
        }

        public override async Task<RequestResult> Handle(DeleteFile request, CancellationToken cancellationToken)
        {
            var file = await _dbContext.Files.FirstOrDefaultAsync(e => e.Id == request.Id);
            if (file == null)
                return Error(RequestResultErrorCode.NotFound);

            var deleteResult = await _fileStorage.DeleteFile(file.FileLocationId.ToString());
            if (deleteResult)
                return Success();

            return Error(RequestResultErrorCode.NotFound);
        }
    }
}
