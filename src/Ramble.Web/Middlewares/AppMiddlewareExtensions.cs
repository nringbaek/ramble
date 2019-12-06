using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ramble.Data;

namespace Ramble.Web.Middlewares
{
    public static class AppMiddlewareExtensions
    {
        public static void UseInitialSetupMiddleware(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<RambleDbContext>();
            if (InitialSetupMiddleware.ShouldPerformInitialSetup(dbContext).GetAwaiter().GetResult())
                app.UseMiddleware<InitialSetupMiddleware>();
        }
    }
}
