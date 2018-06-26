using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Wrap;

namespace TestCircuitBreaker.Services
{

    public interface IAService
    {
        Task<string> GetAsync();
    }

    public class AService : IAService
    {
        private readonly PolicyWrap<string> _policyWrap;

        private readonly ILogger _logger;

        public AService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AService>();

            var timeout = Policy.TimeoutAsync(1, Polly.Timeout.TimeoutStrategy.Pessimistic, (context, ts, task) =>
            {
                _logger.LogInformation("AService timeout");
                return Task.CompletedTask;
            });

            var fallback = Policy.Handle<Exception>()
                                 .Fallback(() => GetFallBack());

            //var retry = Policy.Handle<Exception>()
            //    .RetryAsync(3, (exception, retryCount) =>
            //    {
            //        _logger.LogInformation("AService Retry");
                    
            //    });

            //var retry = Policy.Handle<Exception>()
            //    .WaitAndRetryAsync(new[]
            //    {
            //        TimeSpan.FromSeconds(1),
            //        TimeSpan.FromSeconds(2),
            //        TimeSpan.FromSeconds(4)
            //    }, (exception, span) =>
            //        {
            //            _logger.LogInformation(
            //                $"AService Retry Exception: {exception.GetBaseException().Message}, span: {span}");
            //        });


            var circuitBreaker = Policy.Handle<Exception>()
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(5),
                    (ex, ts) =>
                    {
                        _logger.LogInformation($"AService OnBreak -- ts = {ts.Seconds}s, ex.message = {ex.Message}");
                    }, () =>
                    {
                        _logger.LogInformation("AService OnRest");
                    });
            _policyWrap = Policy<string>.Handle<Exception>()
                .FallbackAsync(GetFallBack(), (x) =>
                {
                    _logger.LogInformation($"AService Fallback -- {x.Exception.Message}");
                    return Task.CompletedTask;
                })
                .WrapAsync(circuitBreaker)
                .WrapAsync(timeout);
            //.WrapAsync(retry);

        }


        private string GetFallBack()
        {
            return "fallback";
        }

        public async Task<string> GetAsync()
        {
            return await _policyWrap.ExecuteAsync(QueryAsync);
        }


        private async Task<string> QueryAsync()
        {
            using (var client = new HttpClient())
            {
                _logger.LogInformation($"AService QueryAsync: http://localhost:53248/api/values");
                var res = await client.GetStringAsync("http://localhost:53248/api/values");
                return res;
            }
        }
    }
}

