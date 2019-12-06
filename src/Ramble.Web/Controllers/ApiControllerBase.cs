using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ramble.Web.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        public IMediator Mediator { get; }
        public ILogger<ApiControllerBase> Logger { get; }

        public ApiControllerBase(IMediator mediator, ILogger<ApiControllerBase> logger)
        {
            Mediator = mediator;
            Logger = logger;
        }
    }
}
