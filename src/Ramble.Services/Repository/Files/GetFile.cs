using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ramble.Common;
using Ramble.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Ramble.Services.Repository.Files
{
    public class GetFile : Request<GetFile, GetFileResult>
    {
        public int Id { get; }

        public GetFile(int id)
        {
            Id = id;
        }
    }

    public class GetFileResult
    {
        public string Filename { get; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Raw bytes for file data")]
        public byte[] Data { get; }

        public GetFileResult(string filename, byte[] data)
        {
            Filename = filename;
            Data = data;
        }
    }

    public class GetFileHandler : RequestHandler<GetFile, GetFileResult>
    {
        private readonly IFileStorage _fileStorage;
        private readonly RambleDbContext _dbContext;

        public GetFileHandler(IFileStorage fileStorage, RambleDbContext dbContext, ILogger<RequestHandler<GetFile, GetFileResult>> logger) : base(logger)
        {
            _fileStorage = fileStorage;
            _dbContext = dbContext;
        }

        public override async Task<RequestResult<GetFileResult>> Handle(GetFile request, CancellationToken cancellationToken)
        {
            var fileEntity = await _dbContext.Files.FirstOrDefaultAsync(e => e.Id == request.Id);
            if (fileEntity == null)
                return Error(RequestResultErrorCode.NotFound);

            var file = await _fileStorage.TryGetFile(fileEntity.FileLocationId.ToString());
            if (file == null)
                return Error(RequestResultErrorCode.NotFound);

            return Success(new GetFileResult
            (
                filename: fileEntity.Filename,
                data: file
            ));
        }
    }
}
