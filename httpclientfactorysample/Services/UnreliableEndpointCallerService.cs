using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace httpclientfactorysample.Services
{
    public class UnreliableEndpointCallerService
    {
        public HttpClient Client { get; }

        public UnreliableEndpointCallerService(HttpClient client)
        {
            Client = client;
        }

        public async Task<string> GetDataFromUnreliableEndpoint(string requestUrl)
        {
            var response = await Client.GetAsync(requestUrl);

            return response.IsSuccessStatusCode ? "Succeeded" : "Failed";
        }
    }
}
