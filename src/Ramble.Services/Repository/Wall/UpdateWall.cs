using Microsoft.Extensions.Logging;
using Ramble.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ramble.Services.Repository.Wall
{
    public class UpdateWall : Request<UpdateWall>
    {
        public int WallId { get; set; }
    }

    public class UpdateWallHandler : RequestHandler<UpdateWall>
    {
        private readonly RambleDbContext _context;

        public UpdateWallHandler(RambleDbContext context, ILogger<RequestHandler<UpdateWall>> logger) : base(logger)
        {
            _context = context;
        }

        public override async Task<RequestResult> Handle(UpdateWall request, CancellationToken cancellationToken)
        {
            return Success();
        }
    }
}
