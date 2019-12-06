using Microsoft.AspNetCore.Http;
using Ramble.Common;
using Ramble.Common.Core;
using System;
using System.Linq;
using System.Security.Claims;

namespace Ramble.Web.Services
{
    public class HttpRequestContext : IRequestContext
    {
        public RequestIdentity Identity { get; }
        public IServiceProvider ServiceProvider { get; }

        public HttpRequestContext(IHttpContextAccessor contextAccessor)
        {
            ServiceProvider = contextAccessor.HttpContext.RequestServices;
            Identity = new RequestIdentity
            {
                UserId = contextAccessor.HttpContext.User.Identity.Name,
                IsAuthenticated = contextAccessor.HttpContext.User.Identity.IsAuthenticated,
                Roles = contextAccessor.HttpContext.User.Claims
                    .Where(e => e.Type == ClaimTypes.Role)
                    .Select(e => e.Value)
                    .ToArray()
            };
        }
    }
}
