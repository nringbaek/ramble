using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace $rootnamespace$
{
    public class $safeitemname$ : Request<$safeitemname$, $safeitemname$Result>
    {
        public $safeitemname$()
        {
        }
    }

    public class $safeitemname$Result
    {
    }

    public class $safeitemname$Handler : RequestHandler<$safeitemname$, $safeitemname$Result>
    {
        public $safeitemname$Handler(ILogger<$safeitemname$Handler> logger) : base(logger)
        {
        }

        public override Task<RequestResult<$safeitemname$Result>> Handle($safeitemname$ request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}