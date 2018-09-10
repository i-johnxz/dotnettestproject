using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ServerSentEventsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseEnvironment("Development");
    }
}
