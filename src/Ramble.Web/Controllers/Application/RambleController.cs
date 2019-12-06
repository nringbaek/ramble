using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ramble.Services.Core.Files;
using Ramble.Services.Repository.Files;
using System.Threading.Tasks;

namespace Ramble.Web.Controllers.Application
{
    [Route("api/v1/ramble")]
    public class RambleController : ApiControllerBase
    {
        public RambleController(IMediator mediator, ILogger<ApiControllerBase> logger) : base(mediator, logger)
        {
        }

        [HttpGet("file/{id}")]
        public async Task<ActionResult> GetFile(int id)
        {
            var result = await Mediator.Send(new GetFile(id));
            if (result.IsError)
                return NotFound();

            return File(result.Value.Data, FileUtility.GetFileContentType(result.Value.Filename));
        }
    }
}
