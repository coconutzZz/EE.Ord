using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EE.Ord.Main.App.Client.Infrastructure.Abstractions.Cache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace EE.Ord.Main.App.Client.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInMemoryCache(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<ICache, InMemoryCache>();
        }

        public static void AddInMemoryCache(this IServiceCollection services, Action<MemoryCacheOptions> setupAction)
        {
            services.AddMemoryCache(setupAction);
            services.AddScoped<ICache, InMemoryCache>();
        }
    }
}
