using Microsoft.Extensions.Logging;
using Ramble.Common;
using Ramble.Data;
using Ramble.Data.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ramble.Services.Repository.Files
{
    public class AddFile : Request<AddFile>
    {
        public byte[] Data { get; set; }
        public string Filename { get; set; }
    }

    public class AddFileHandler : RequestHandler<AddFile>
    {
        private readonly IFileStorage _fileStorage;
        private readonly RambleDbContext _dbContext;

        public AddFileHandler(IFileStorage fileStorage, RambleDbContext dbContext, ILogger<RequestHandler<AddFile>> logger) : base(logger)
        {
            _fileStorage = fileStorage;
            _dbContext = dbContext;
        }

        public override async Task<RequestResult> Handle(AddFile request, CancellationToken cancellationToken)
        {
            var fileEntity = new FileEntity
            {
                Filename = request.Filename,
                FileLocationId =  Guid.NewGuid(),
                Created = DateTimeOffset.Now
            };

            _dbContext.Files.Add(fileEntity);

            await Task.WhenAll(
                _dbContext.SaveChangesAsync(),
                _fileStorage.AddFile(fileEntity.FileLocationId.ToString(), request.Data)
            );

            return Success();
        }
    }
}
