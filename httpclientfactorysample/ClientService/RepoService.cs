using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace httpclientfactorysample.ClientService
{
    public class RepoService
    {
        private readonly HttpClient _httpClient;

        public RepoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<string>> GetRepos()
        {
            var response = await _httpClient.GetAsync("orgs/aspnet/repos");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<IEnumerable<string>>();

            return result;
        }
    }
}
