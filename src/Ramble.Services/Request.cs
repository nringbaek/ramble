using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ramble.Services
{
    public class Request : IRequest<RequestResult> { }
    public class Request<TResult> : IRequest<RequestResult<TResult>> { }
}
