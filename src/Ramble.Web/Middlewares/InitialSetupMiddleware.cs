using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Ramble.Data;
using System.Threading.Tasks;

namespace Ramble.Web.Middlewares
{
    public class InitialSetupMiddleware : IMiddleware
    {
        private static bool _requiresSetupCheck = true;
        private readonly RambleDbContext _dbContext;

        public InitialSetupMiddleware(RambleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (_requiresSetupCheck)
            {
                var hasUsers = await _dbContext.Users.AnyAsync();
                if (hasUsers)
                {
                    _requiresSetupCheck = false;
                    await next(context);
                }
                else
                {
                    if (context.Request.Path.StartsWithSegments("/initialsetup"))
                        await next(context);
                    else
                        context.Response.Redirect("/initialsetup");
                }
            }
            else
                await next(context);
        }
    }
}
