using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Ramble.Data;
using Ramble.Data.Models;
using Ramble.Web.Areas.Identity.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;

namespace Ramble.Web.Areas.Identity
{
    public static class IdentityConfigurationBuilder
    {
        public static void AddRambleIdentity(this IServiceCollection services, IdentityConfiguration configuration)
        {
            services.AddSingleton(configuration);

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            var migrationsAssembly = typeof(IdentityConfigurationBuilder).GetTypeInfo().Assembly.GetName().Name;

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

            services.AddIdentityServer()
                .AddAspNetIdentity<RambleUserEntity>()
                .AddInMemoryApiResources(IdpResources.GetApis())
                .AddInMemoryIdentityResources(IdpResources.GetIdentityResources())
                .AddInMemoryClients(IdpResources.GetClients(configuration.BaseUrl, configuration.Secret))
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(configuration.DbConnectionString, sql =>
                            sql.MigrationsAssembly(migrationsAssembly));

                    options.EnableTokenCleanup = true;
                });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies", options =>
            {
                options.LoginPath = "/idp/account/login";
                options.LogoutPath = "/idp/account/logout";
                options.AccessDeniedPath = "/idp/account/accessdenied";
                options.SlidingExpiration = true;

                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "RambleIDP";
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            })
            .AddOpenIdConnect("oidc", options =>
            {
                options.SignInScheme = "Cookies";
                options.Authority = configuration.BaseUrl;

                options.ClientId = "ramble.admin.client";
                options.ClientSecret = "secret";
                options.ResponseType = "code id_token";

                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                
                options.Scope.Add("ramble.api");
                options.Scope.Add("ramble.admin.api");
                options.Scope.Add("offline_access");
            });
        }

        public static void UseRambleIdentity(this IApplicationBuilder app, IWebHostEnvironment hostEnvironment)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/idp/content",
                FileProvider = new PhysicalFileProvider(Path.Combine(hostEnvironment.ContentRootPath, "Areas", "Identity", "UI", "Shared", "Content"))
            });

            var configuration = app.ApplicationServices.GetRequiredService<IdentityConfiguration>();
            foreach (var protectedEndpoint in configuration.ProtectedEndpoints)
            {
                app.Map(new PathString(protectedEndpoint), builder =>
                {
                    builder.Use(async (context, next) =>
                    {
                        if (context.User.Identity.IsAuthenticated)
                        {
                            // TODO: Check if we need to refresh token

                            await next();
                        }
                        else
                            await context.ChallengeAsync();
                    });
                });
            }
        }
    }
}
