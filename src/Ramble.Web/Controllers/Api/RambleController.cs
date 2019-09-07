using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ramble.Web.Controllers.Api
{
    [Route("api/v1/ramble")]
    public class RambleController : ApiControllerBase
    {
        public RambleController(IMediator mediator, ILogger<ApiControllerBase> logger) : base(mediator, logger)
        {
        }
    }
}
