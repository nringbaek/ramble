using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Ramble.Web.Controllers.Management
{
    [Route("api/v1/management/session")]
    public class SessionController : ApiControllerBase
    {
        public SessionController(IMediator mediator, ILogger<SessionController> logger) : base(mediator, logger)
        {
        }

        [Authorize]
        [HttpGet("")]
        public async Task<ActionResult> GetSession()
        {
            var bearerToken = await HttpContext.GetTokenAsync("access_token");

            return Ok(new
            {
                ShowFrontpage = true,
                BearerToken = bearerToken,
                Username = User.Identity.Name
            });
        }

        [HttpGet("challenge")]
        public ActionResult Challenge(string redirecturl)
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = redirecturl
            }, RambleConstants.AuthenticationSchemes.Oidc);
        }

        [HttpPost("signout")]
        public ActionResult SignOut()
        {
            return SignOut(
                RambleConstants.AuthenticationSchemes.Cookies,
                RambleConstants.AuthenticationSchemes.Oidc
            );
        }
    }
}
