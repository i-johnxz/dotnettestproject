using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundatio.Caching;
using Foundatio.Lock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace FoundatioDemo
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection AddFundatioService(this IServiceCollection services)
        {

            var muxer = ConnectionMultiplexer.Connect(IFramework.Config.Configuration.Instance["RedisConnection"]);
            services.AddSingleton<ICacheClient>(s => new RedisHybridCacheClient(o => o.ConnectionMultiplexer(muxer)));
            services.AddSingleton<ILockProvider>(s =>
                new ThrottlingLockProvider(s.GetRequiredService<ICacheClient>(), 1, TimeSpan.FromSeconds(90)));

            return services;
        }
    }
}
