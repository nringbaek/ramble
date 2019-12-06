using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ramble.Common;
using Ramble.Data;
using Ramble.Services.Pipeline;
using System;

namespace Ramble.Services.DependencyInjection
{
    public static class DependencyBuilder
    {
        public static RambleServiceBuilder AddRambleCoreServices(this IServiceCollection services, Action<RambleServiceBuilderOptions> options)
        {
            var configuredOtions = new RambleServiceBuilderOptions();
            options.Invoke(configuredOtions);

            if (configuredOtions.Services == null)
                configuredOtions.Services = services;

            services.AddSingleton(options);
            SetupRambleCoreServices(services, configuredOtions);

            return new RambleServiceBuilder(configuredOtions);
        }

        private static void SetupRambleCoreServices(IServiceCollection services, RambleServiceBuilderOptions options)
        {
            services.AddMediatR(typeof(DependencyBuilder));

            if (options.Pipeline.EnableRequestValidation)
                services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

            if (options.Pipeline.EnableRequestAuthorization)
                services.AddScoped(typeof(IPipelineBehavior<,>), typeof(AuthorizationPipeline<,>));
        }

        public static RambleServiceBuilder AddFileStorage<TFileStorage, TFileStorageOptions>(
            this RambleServiceBuilder builder, Action<TFileStorageOptions> configureOptions
            ) where TFileStorage : IFileStorage where TFileStorageOptions : class
        {
            builder.Options.Services.Configure(configureOptions);
            builder.Options.Services.AddScoped(typeof(IFileStorage), typeof(TFileStorage));

            return builder;
        }

        public static RambleServiceBuilder AddRequestContext<TRequestContext>(
            this RambleServiceBuilder builder
            ) where TRequestContext : IRequestContext
        {
            builder.Options.Services.AddScoped(typeof(IRequestContext), typeof(TRequestContext));
            return builder;
        }
    }
}
