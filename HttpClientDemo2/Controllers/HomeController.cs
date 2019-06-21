using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HttpClientDemo2.Models;

namespace HttpClientDemo2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            //using (var file = System.IO.File.OpenRead("~/1.pdf"))
            //using (var memoryStream = new MemoryStream())
            //{
            //    file.CopyTo(memoryStream, 0);
            //}
            byte[] postBytes =
                System.IO.File.ReadAllBytes(
                    @"E:\workspace\demo\test\dotnettestproject\dotnettestproject\HttpClientDemo2\wwwroot\2.pdf");
            string boundary = $"----{DateTime.Now.Ticks:x}";
            var httpClient = _httpClientFactory.CreateClient();
            var req = new HttpRequestMessage(HttpMethod.Post, "http://www.example.com/");
            var req2 = new HttpRequestMessage(HttpMethod.Post, "http://www.example.com/");

            var content1 = new MultipartFormDataContent(boundary)
            {
                new ByteArrayContent(postBytes)
            };

            var content2 = new MultipartContent("form-data", boundary)
            {
                new ByteArrayContent(postBytes)
            };

            req.Content = content1;
            req2.Content = content2;

            using (var response = httpClient.SendAsync(req).ConfigureAwait(false).GetAwaiter().GetResult())
            {
                var responseText = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            }

            using (var response2 = httpClient.SendAsync(req).ConfigureAwait(false).GetAwaiter().GetResult())
            {
                var responseText2 = response2.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter()
                    .GetResult();
            }


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
