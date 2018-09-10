using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IFramework.Infrastructure;
using Microsoft.Extensions.Logging;

namespace RefitDemo3
{
    public class ApiHttpHandler : DelegatingHandler
    {
        private readonly ILogger _logger;

        public ApiHttpHandler(ILogger<ApiHttpHandler> logger)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var startDate = DateTime.Now;

            var response = await base.SendAsync(request, cancellationToken);

            var requestBody = request.Content != null
                ? await request.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false)
                : null;

            var endDate = DateTime.Now;

            var costTime = (endDate - startDate).TotalMilliseconds;

            var responseBody = request.Method != HttpMethod.Get && response.Content != null
                ? await response.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false)
                : null;

            if (!response.IsSuccessStatusCode)
            {
                _logger?.LogError(new { request.RequestUri, request.Method, responseBody, costTime }.ToJson());
                throw new Exception(responseBody);
            }

            _logger?.LogInformation(new { request.RequestUri, request.Method, requestBody, responseBody, costTime }.ToJson());

            return response;
        }
    }
}
