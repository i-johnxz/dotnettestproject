using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using log4net;
using LL.Core.Configurations;
using LL.Logging.Log4Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Xunit;
using Xunit.Abstractions;

namespace TestJob
{
    public class UnitTest1
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly ITestOutputHelper _output;
        

        public UnitTest1(ITestOutputHelper output)
        {
            _output = output;
            //Configuration.Instance.UseLog4Net();
            var services = new ServiceCollection();
            services.UseLog4Net();
            _serviceProvider = services.BuildServiceProvider();

        }

        [Fact]
        public async Task Test1()
        {
            await RunProgram(_output);
        }

        private static async Task RunProgram(ITestOutputHelper output)
        {
            try
            {
                LogProvider.SetCurrentLogProvider(new ConsoleLogProvider(output));
                NameValueCollection props = new NameValueCollection
                {
                    {"quartz.serializer.type", "binary"}
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();

                await scheduler.Start();

                await Task.Delay(TimeSpan.FromSeconds(60));

                await scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }

        private class ConsoleLogProvider : ILogProvider
        {
            private readonly ITestOutputHelper _output;

            public ConsoleLogProvider(ITestOutputHelper output)
            {
                _output = output;
            }

            public Logger GetLogger(string name)
            {
                return (level, func, exception, parameters) =>
                {
                    if (level >= Quartz.Logging.LogLevel.Info && func != null)
                    {
                        _output.WriteLine("[" + DateTime.Now.ToLongTimeString() + "][" + level + "]" + func(),
                            parameters);
                    }
                    return true;
                };
            }

            public IDisposable OpenNestedContext(string message)
            {
                throw new NotImplementedException();
            }

            public IDisposable OpenMappedContext(string key, string value)
            {
                throw new NotImplementedException();
            }
        }
    }


}

