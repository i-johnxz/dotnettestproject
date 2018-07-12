using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using httpclientfactorysample.ClientService;
using Microsoft.AspNetCore.Mvc;
using httpclientfactorysample.Models;

namespace httpclientfactorysample.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IHttpClientFactory _clientFactory;

        //public HomeController(IHttpClientFactory clientFactory)
        //{
        //    _clientFactory = clientFactory;
        //}
        //private readonly GitHubService _gitHubService;

        //public HomeController(GitHubService gitHubService)
        //{
        //    _gitHubService = gitHubService;
        //}

        //private readonly RepoService _repoService;

        //public HomeController(RepoService repoService)
        //{
        //    _repoService = repoService;
        //}


        public IActionResult Index()
        {
            //var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/repos/aspnet/docs/branches");
            //request.Headers.Add("Accept", "application/vnd.github.v3+json");
            //request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            //var client = _clientFactory.CreateClient();

            //var response = await client.SendAsync(request);
            //IEnumerable<GitHubBranch> branches;

            //if (response.IsSuccessStatusCode)
            //{
            //    branches = await response.Content.ReadAsAsync<IEnumerable<GitHubBranch>>();
            //}
            //else
            //{
            //    branches = Array.Empty<GitHubBranch>();
            //}

            return View();
        }

        public IActionResult About()
        {
            //var request = new HttpRequestMessage(HttpMethod.Get, "repos/aspnet/docs/pulls");

            //var client = _clientFactory.CreateClient("github");

            //IEnumerable<GitHubPullRequest> pullRequests;

            //var response = await client.SendAsync(request);

            //if (response.IsSuccessStatusCode)
            //{
            //    pullRequests = await response.Content.ReadAsAsync<IEnumerable<GitHubPullRequest>>();
            //}
            //else
            //{
            //    pullRequests = Array.Empty<GitHubPullRequest>();
            //}


            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
