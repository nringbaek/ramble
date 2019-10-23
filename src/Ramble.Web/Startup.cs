using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ramble.Data;
using Ramble.Web.Areas.Identity;
using Ramble.Web.Middlewares;
using System;
using System.IO;
using System.Net.Http;

namespace Ramble.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<RambleDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("RambleDbContext")));

            services.AddDataProtection()
                .ProtectKeysWithDpapi()
                .SetApplicationName("Ramble")
                .PersistKeysToFileSystem(new DirectoryInfo(Configuration["DataProtection:Storage"]));

            services.AddRambleIdentity(Configuration.GetSection("IdentityConfiguration").Get<IdentityConfiguration>());

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("~/Areas/{2}/UI/{1}/Views/{0}.cshtml");
                options.AreaViewLocationFormats.Add("~/Areas/{2}/UI/Shared/Views/{0}.cshtml");
            });

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            services.AddRazorPages()
                .AddRazorRuntimeCompilation();

            services.AddTransient<InitialSetupMiddleware>();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<InitialSetupMiddleware>();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRambleIdentity(Environment);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}"
                );
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (Environment.IsDevelopment())
                {
                    try
                    {
                        // Use local proxy if a 'ng serve' instance is running
                        var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };
                        var result = httpClient.GetAsync("http://localhost:4200/").GetAwaiter().GetResult();
                        if (result.IsSuccessStatusCode)
                            spa.UseProxyToSpaDevelopmentServer("http://localhost:4200/");
                        else
                            spa.UseAngularCliServer(npmScript: "start");
                    }
                    catch { spa.UseAngularCliServer(npmScript: "start"); }
                }
            });
        }
    }
}
