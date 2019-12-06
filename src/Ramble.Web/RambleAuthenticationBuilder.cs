using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Ramble.Data;
using Ramble.Data.Models;
using System.Threading.Tasks;

namespace Ramble.Web
{
    public static class RambleAuthenticationBuilder
    {
        public static void AddRambleAuthentication(this IServiceCollection services)
        {
            services.AddIdentity<RambleUserEntity, RambleUserRoleEntity>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<RambleDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ReturnUrlParameter = "returnurl";
                    options.LoginPath = "_authentication/signin";
                    options.LogoutPath = "_authentication/signout";
                    options.AccessDeniedPath = "_authentication/accessdenied";

                    options.Cookie.Name = "ruid";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;

                    options.Events.OnRedirectToLogin = ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api/"))
                            ctx.Response.StatusCode = 401;

                        return Task.CompletedTask;
                    };
                });
        }
    }
}
