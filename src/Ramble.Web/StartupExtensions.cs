using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ramble.Web
{
    public static class StartupExtensions
    {
        public static IMvcBuilder When(this IMvcBuilder builder, bool shouldAdd, Action<IMvcBuilder> addAction)
        {
            if (shouldAdd)
                addAction(builder);

            return builder;
        }

        public static IApplicationBuilder When(this IApplicationBuilder builder, bool shouldAdd, Action<IApplicationBuilder> addAction)
        {
            if (shouldAdd)
                addAction(builder);

            return builder;
        }
    }
}
