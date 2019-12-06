using Microsoft.Extensions.Logging;
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
        public UpdateWallHandler(ILogger<RequestHandler<UpdateWall>> logger) : base(logger)
        {
        }

        public override async Task<RequestResult> Handle(UpdateWall request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
