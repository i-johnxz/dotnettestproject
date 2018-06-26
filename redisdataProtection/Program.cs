using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace redisdataProtection
{
    class Program
    {
        static void Main(string[] args)
        {
            var redis = ConnectionMultiplexer.Connect("172.16.200.229:6379,defaultDatabase=1");

            //var serviceCollection = new ServiceCollection();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            serviceCollection.AddDataProtection()
                .PersistKeysToRedis(redis, "DataProtection-Keys");

            var services = serviceCollection.BuildServiceProvider();
            

            // Run a sample payload
            var protector = services.GetDataProtector("sample-purpose");
            var protectedData = protector.Protect("Hello world!");
            Console.WriteLine(protectedData);
        }
    }
}
