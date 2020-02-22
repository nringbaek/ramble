using MediatR;
using Ramble;
using Ramble.Common;
using Ramble.Services.DependencyInjection;
using Ramble.Services.Pipeline;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RambleCoreBuilderExtensions
    {
        public static RambleServiceBuilder AddRambleCoreServices<TContext>(this IServiceCollection services) where TContext : IRequestContext =>
            services.AddRambleCoreServices<TContext>(options => { });

        public static RambleServiceBuilder AddRambleCoreServices<TContext>(this IServiceCollection services, Action<RambleServiceCoreOptions> options) where TContext : IRequestContext
        {
            var rambleOptions = new RambleServiceCoreOptions();
            options.Invoke(rambleOptions);
            
            services.AddSingleton(options);
            services.AddScoped(typeof(IRequestContext), typeof(TContext));

            services.AddMediatR(typeof(RambleCoreBuilderExtensions));

            if (rambleOptions.Pipeline.EnableRequestValidation)
                services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

            if (rambleOptions.Pipeline.EnableRequestAuthorization)
                services.AddScoped(typeof(IPipelineBehavior<,>), typeof(AuthorizationPipeline<,>));

            return new RambleServiceBuilder(services);
        }

        public static RambleServiceBuilder AddFileStorage<TFileStorage, TFileStorageOptions>(
            this RambleServiceBuilder builder, Action<TFileStorageOptions> configureOptions
            ) where TFileStorage : IFileStorage where TFileStorageOptions : class
        {
            builder.Services.Configure(configureOptions);
            builder.Services.AddScoped(typeof(IFileStorage), typeof(TFileStorage));

            return builder;
        }
    }
}
