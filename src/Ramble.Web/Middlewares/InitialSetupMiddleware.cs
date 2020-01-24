using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Ramble.Data;
using System;
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
                if (await ShouldPerformInitialSetup(_dbContext))
                {
                    if (context.Request.Path.StartsWithSegments("/initialsetup", StringComparison.InvariantCultureIgnoreCase))
                        await next(context);
                    else
                        context.Response.Redirect("/initialsetup");
                }
                else
                {
                    _requiresSetupCheck = false;
                    await next(context);
                }
            }
            else
                await next(context);
        }

        public static async Task<bool> ShouldPerformInitialSetup(RambleDbContext context) =>
            await context.Users.AnyAsync() == false;
    }
}
