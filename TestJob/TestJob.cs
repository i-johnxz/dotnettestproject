using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.WebUtilities;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Xunit;
using Xunit.Abstractions;

namespace TestJob
{
    public class TestJob
    {
        private readonly ITestOutputHelper _output;
        public TestJob(ITestOutputHelper output)
        {
            _output = output;
        }


        [Fact]
        public void Test_Query()
        {
            var redirectUri = "http://www.baidu.com?";
            var queryString = QueryHelpers.ParseQuery(new Uri(redirectUri).Query);
            var sessionId = "123123";
            var UserId = "34567123";
            var queryAuthInformation = $"sessionId={sessionId}&weChatUserId={UserId}";
            if (queryString.Count > 0)
            {
                var newQuery = string.Join("&", queryString.Where(q => q.Key.ToLower() != "sessionid" && q.Key.ToLower() != "wechatuserid")
                    .Select(q => $"{HttpUtility.UrlEncode(q.Key)}={HttpUtility.UrlEncode(q.Value)}"));
                redirectUri = $"{redirectUri.Substring(0, redirectUri.IndexOf("?", StringComparison.Ordinal))}?{newQuery}&{queryAuthInformation}";
            }
            else
            {
                redirectUri = redirectUri.EndsWith("?") ? $"{redirectUri}{queryAuthInformation}" : $"{redirectUri}?{queryAuthInformation}";
            }

            _output.WriteLine(redirectUri);
            Assert.NotEmpty(redirectUri);

        }


        [Fact]
        public async Task Test_Job()
        {
            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider(_output));
            await RunProgramRunExample();

            _output.WriteLine("Press any key to close the application");
            Console.ReadKey();
        }

        private static async Task RunProgramRunExample()
        {
            try
            {
                NameValueCollection props = new NameValueCollection
                {
                    {"quartz.serializer.type", "binary"}
                };

                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = await factory.GetScheduler();

                await scheduler.Start();

                IJobDetail job = JobBuilder.Create<HelloJob>()
                                           .WithIdentity("job1","group1")
                                           .Build();

                ITrigger trigger = TriggerBuilder.Create()
                                                 .WithIdentity("trigger1", "group1")
                                                 .StartNow()
                                                 .WithSimpleSchedule(x => x.WithIntervalInSeconds(10)
                                                 .RepeatForever())
                                                 .Build();

                await scheduler.ScheduleJob(job, trigger);

                await Task.Delay(TimeSpan.FromSeconds(60));

                await scheduler.Shutdown();

            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
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
                    if (level >= LogLevel.Info && func != null)
                    {
                        _output.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + level + "] " + func(), parameters);
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

        public class HelloJob : IJob
        {
            private readonly ITestOutputHelper _output;

            public HelloJob(ITestOutputHelper output)
            {
                _output = output;
            }

            public async Task Execute(IJobExecutionContext context)
            {
                _output.WriteLine("Greetings from HelloJob!");
            }
        }
    }
}
