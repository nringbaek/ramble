using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Ramble.Web.Controllers.Api
{
    [Route("api/v1/configuration")]
    public class ConfigurationController : ApiControllerBase
    {
        public ConfigurationController(IMediator mediator, ILogger<ConfigurationController> logger) : base(mediator, logger)
        {
        }

        [HttpGet("")]
        public async Task<ActionResult> GetConfiguration()
        {
            var bearerToken = await HttpContext.GetTokenAsync("access_token");

            return Ok(new
            {
                ShowFrontpage = true,
                BearerToken = bearerToken
            });
        }
    }
}
