using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ramble.Web.Controllers.Api
{
    [Route("api/v1/configuration")]
    public class ConfigurationController : ApiControllerBase
    {
        public ConfigurationController(IMediator mediator, ILogger<ApiControllerBase> logger) : base(mediator, logger)
        {
        }

        [HttpGet("")]
        public ActionResult GetConfiguration()
        {
            return Ok(new
            {
                ShowFrontpage = true
            });
        }
    }
}
