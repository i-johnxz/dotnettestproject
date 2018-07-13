using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;

namespace httpclientfactorysample3
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().GetAwaiter().GetResult();
        }


        public static async Task Run()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(b =>
            {
                b.AddFilter((category, level) => true);
                b.AddConsole(c => c.IncludeScopes = true);
            });

            Configure(serviceCollection);

            var services = serviceCollection.BuildServiceProvider();

            Console.WriteLine("Creating a client....");

            var github = services.GetRequiredService<GithubClient>();

            Console.WriteLine("Sending a request....");

            var response = await github.GetJson();

            var data = await response.Content.ReadAsAsync<object>();

            Console.WriteLine("Response data:");
            Console.WriteLine(data);

            Console.WriteLine("Press the ANY key to exit...");
            Console.ReadKey();
        }

        public static void Configure(IServiceCollection services)
        {
            var registry = services.AddPolicyRegistry();

            var timeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10));
            var longTimeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(30));

            registry.Add("regular", timeout);
            registry.Add("long", longTimeout);

            services.AddHttpClient("github", c =>
                {
                    c.BaseAddress = new Uri("https://api.github.com/");

                    c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                    c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
                })
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)))
                .AddPolicyHandlerFromRegistry("regular")
                .AddPolicyHandler((request) => request.Method == HttpMethod.Get ? timeout : longTimeout)
                .AddPolicyHandlerFromRegistry((reg, request) => request.Method == HttpMethod.Get
                    ? reg.Get<IAsyncPolicy<HttpResponseMessage>>("regular")
                    : reg.Get<IAsyncPolicy<HttpResponseMessage>>("long"))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync())
                .AddHttpMessageHandler(() => new RetryHandler())
                .AddTypedClient<GithubClient>();
        }

        private class GithubClient
        {
            public GithubClient(HttpClient httpClient)
            {
                HttpClient = httpClient;
            }

            private HttpClient HttpClient { get; }

            public async Task<HttpResponseMessage> GetJson()
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "/");

                var response = await HttpClient.SendAsync(request).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return response;
            }
        }

        public class RetryHandler : DelegatingHandler
        {
            public int RetryCount { get; set; } = 5;

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                for (int i = 0; i < RetryCount; i++)
                {
                    try
                    {
                        return await base.SendAsync(request, cancellationToken);
                    }
                    catch (HttpRequestException) when (i == RetryCount - 1)
                    {
                        throw;
                    }
                    catch (HttpRequestException)
                    {
                        await Task.Delay(TimeSpan.FromMilliseconds(50), cancellationToken);
                    }
                }

                return null;
            }
        }
    }
}
