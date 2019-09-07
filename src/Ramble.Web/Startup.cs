using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ramble.Data;
using Ramble.Data.Models;
using System;
using System.Net.Http;

namespace Ramble.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContextPool<RambleDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddIdentity<RambleUserEntity, RambleUserRoleEntity>()
              .AddEntityFrameworkStores<RambleDbContext>()
              .AddDefaultTokenProviders();

            //services.AddIdentityServer()
            //   .AddDeveloperSigningCredential()
            //   .AddOperationalStore(options =>
            //   {
            //       // this adds the operational data from DB (codes, tokens, consents)

            //       options.ConfigureDbContext = builder => builder.UseSqlServer(Configuration.GetConnectionString("Default"));
            //       options.EnableTokenCleanup = true;
            //       options.TokenCleanupInterval = 30;
            //   })
            //   .AddInMemoryIdentityResources(Config.GetIdentityResources())
            //   .AddInMemoryApiResources(Config.GetApiResources())
            //   .AddInMemoryClients(Config.GetClients())
            //   .AddAspNetIdentity<RambleUserEntity>();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
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
