using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ramble.Web.Controllers.Management
{
    [Authorize]
    [Route("api/v1/management/file")]
    public class FileController : ApiControllerBase
    {
        public FileController(IMediator mediator, ILogger<ApiControllerBase> logger) : base(mediator, logger)
        {
        }


    }
}
