using HotChocolate;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ramble.Data;
using Ramble.Data.GraphQl;
using Ramble.Data.Infrastructure.Mssql;
using Ramble.Services.Authorization.Rules;
using Ramble.Services.Core.Files;
using Ramble.Web.Middlewares;
using Ramble.Web.Services;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;

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
            services.AddDataProtection()
                .ProtectKeysWithDpapi()
                .SetApplicationName("Ramble")
                .PersistKeysToFileSystem(new DirectoryInfo(Configuration["DataProtection:Storage"]));

            services.AddDbContextPool<RambleDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("RambleDbContext"), sql =>
                    sql.MigrationsAssembly(typeof(InfrastructureMssqlExtensions).GetTypeInfo().Assembly.GetName().Name));
            });

            services.AddHttpContextAccessor();
            services.AddResponseCompression();
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            services.AddControllers();
            services.AddRazorPages()
                .When(Environment.IsDevelopment(), builder => builder.AddRazorRuntimeCompilation());

            services.AddRambleAuthentication();
            services.AddRambleCoreServices<HttpRequestContext>(options =>
            {
                options.Pipeline.GlobalAuthorizationRules.Add(new IsAuthenticatedRule());
            })
            .AddFileStorage<LocalFileStorage, LocalFileStorageOptions>(options => options.BasePath = Configuration.GetValue<string>("RambleConfiguration:Storage"))
            .AddStandaloneFeature();

            services.AddTransient<InitialSetupMiddleware>();

            services.AddDataLoaderRegistry();
            services.AddGraphQL(sp => SchemaBuilder.New()
                .AddQueryType<RambleQuery>()
                .AddServices(sp)
                .Create()
            );

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

            app.UseStaticFiles();
            app.When(!Environment.IsDevelopment(), a => a.UseSpaStaticFiles());

            app.UseResponseCompression();
            app.UseInitialSetupMiddleware();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWebSockets();
            app.UseGraphQL("/api/graphql")
                .UseGraphiQL("/api/graphql");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                
                if (Environment.IsDevelopment())
                {
                    try
                    {
                        // Use local proxy if 'ng serve' instance is running
                        var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };
                        var result = httpClient.GetAsync(new Uri("http://localhost:4200/")).GetAwaiter().GetResult();
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
