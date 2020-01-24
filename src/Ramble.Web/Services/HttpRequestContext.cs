using Microsoft.AspNetCore.Http;
using Ramble.Common;
using Ramble.Common.Core;
using System;
using System.Collections.Generic;
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
            Identity = contextAccessor.HttpContext.User.Identity.IsAuthenticated
                ? GetAuthenticatedIdentity(contextAccessor)
                : RequestIdentity.Anonymous();
        }

        private RequestIdentity GetAuthenticatedIdentity(IHttpContextAccessor contextAccessor)
        {
            var userId = contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var roles = contextAccessor.HttpContext.User.Claims
                .Where(e => e.Type == ClaimTypes.Role)
                .Select(e => e.Value)
                .ToList();

            var properties = new Dictionary<string, object>
            {
                { "name", contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name) },
                { "email", contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email) }
            };

            return RequestIdentity.Authenticated(
                userId: userId,
                roles: roles,
                properties: properties
            );
        }
    }
}
