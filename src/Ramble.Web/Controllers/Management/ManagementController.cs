using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ramble.Web.Controllers.Management
{
    [Authorize]
    [Route("api/v1/management")]
    public class ManagementController : ApiControllerBase
    {
        public ManagementController(IMediator mediator, ILogger<ApiControllerBase> logger) : base(mediator, logger)
        {
        }
    }
}
