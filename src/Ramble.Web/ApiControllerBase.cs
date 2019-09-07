using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ramble.Web
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
