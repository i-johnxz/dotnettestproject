using System;
using System.Collections.Async;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Xunit;
using Xunit.Abstractions;

namespace ConcurrentThreadsTest
{
    public class ConcurrentThead1
    {
        private readonly ITestOutputHelper _output;

        private readonly string[] urls = new[] {
            "https://github.com/naudio/NAudio",
            "https://twitter.com/mark_heath",
            "https://github.com/markheath/azure-functions-links",
            "https://pluralsight.com/authors/mark-heath",
            "https://github.com/markheath/advent-of-code-js",
            "http://stackoverflow.com/users/7532/mark-heath",
            "https://mvp.microsoft.com/en-us/mvp/Mark%20%20Heath-5002551",
            "https://github.com/markheath/func-todo-backend",
            "https://github.com/markheath/typescript-tetris",
        };

        private HttpClient client = new HttpClient();

        private int maxThreads = 4;

        public ConcurrentThead1(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task GetClient()
        {

            var client = new HttpClient();
            foreach (var url in urls)
            {
                var html = await client.GetStringAsync(url);
                _output.WriteLine($"retrieved {html.Length} characters from {url}");
            }
        }

        [Fact]
        public async Task GetClientByConcurrentQueue()
        {
            var q = new ConcurrentQueue<string>(urls);
            var tasks = new List<Task>();
            for (int n = 0; n < maxThreads; n++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    while (q.TryDequeue(out string url))
                    {
                        var html = await client.GetStringAsync(url);
                        _output.WriteLine($"retrieved {html.Length} characters from {url}");
                    }
                }));
            }
            await Task.WhenAll(tasks);
        }

        [Fact]
        public async Task GetClientBySemaphoreSlim()
        {
            var allTasks = new List<Task>();
            var throttler = new SemaphoreSlim(maxThreads);
            foreach (var url in urls)
            {
                await throttler.WaitAsync();
                allTasks.Add(
                    Task.Run(async () =>
                    {
                        try
                        {
                            var html = await client.GetStringAsync(url);
                            _output.WriteLine($"retrieved {html.Length} characters from {url}");
                        }
                        finally
                        {
                            throttler.Release();
                        }
                    }));
            }
            await Task.WhenAll(allTasks);
        }

        [Fact]
        public void GetClientByParallel()
        {
            var options = new ParallelOptions() { MaxDegreeOfParallelism = maxThreads };


            Parallel.ForEach(urls, options, url =>
            {
                var html = client.GetStringAsync(url).Result;
                _output.WriteLine($"retrieved {html.Length} characters from {url}");
            });
        }

        [Fact]
        public async Task GetClientByParallelAsync()
        {
            await urls.ParallelForEachAsync(
                async url =>
                {
                    var html = await client.GetStringAsync(url);
                    _output.WriteLine($"retrieved {html.Length} characters from {url}");
                }, maxThreads);
        }

        [Fact]
        public async Task GetClientByPolicy()
        {
            var bulkhead = Policy.BulkheadAsync(maxThreads, Int32.MaxValue);
            var tasks = new List<Task>();
            foreach (var url in urls)
            {
                var t = bulkhead.ExecuteAsync(async () =>
                {
                    var html = await client.GetStringAsync(url);
                    _output.WriteLine($"retrieved {html.Length} characters from {url}");
                });
                tasks.Add(t);
            }
            await Task.WhenAll(tasks);
        }
    }
}
