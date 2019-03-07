using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App.Metrics.AspNetCore.Health;
using App.Metrics.Health;
using App.Metrics.Health.Checks.Sql;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BeatPulseHealthCheck
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureHealthWithDefaults(builder =>
                {
                    const long threshold = 8589934592;
                    const long VirtualMemorySize = 2418925581107;
                    //builder.HealthChecks.AddCheck("DatabaseConnected", () => new ValueTask<HealthCheckResult>(HealthCheckResult.Healthy("Database Connection OK")));
                    builder.HealthChecks.AddSqlCheck("Asset", @"Server=(localdb)\projects;Database=Asset;Integrated Security=true;", TimeSpan.FromSeconds(10));
                    builder.HealthChecks.AddProcessPrivateMemorySizeCheck("Private Memory Size", threshold);
                    builder.HealthChecks.AddProcessVirtualMemorySizeCheck("Virtual Memory Size", VirtualMemorySize);
                    builder.HealthChecks.AddProcessPhysicalMemoryCheck("Working Set", threshold);
                })
                .UseHealth()
                .UseStartup<Startup>();
    }
}
