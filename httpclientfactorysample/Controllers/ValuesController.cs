using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using httpclientfactorysample.ClientService;
using httpclientfactorysample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace httpclientfactorysample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //private readonly IHelloClient _client;

        //public ValuesController(IHelloClient client)
        //{
        //    _client = client;
        //}


        private readonly IHttpClientFactory _httpClientFactory;

        public ValuesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public Reply Get()
        {
            //var message = await _client.GetMessageAsync();
            //return message;

            var test = _httpClientFactory.CreateClient("test");
            using (var client = _httpClientFactory.CreateClient())
            {
                    
            }

            return new Reply();

            //var _client = RestService.For<IHelloClient>("http://localhost:5000", new RefitSettings
            //{
            //    HttpMessageHandlerFactory = HttpMessageHandlerFactory
            //});


            //var message = await _client.GetMessageAsync();

            //var _client1 = RestService.For<IHelloClient>("http://localhost:5000");

            //var message2 = await _client1.GetMessageAsync();
            ////return _client.GetMessageAsync();

            //return new String[]
            //{
            //    _client.GetHashCode().ToString(),
            //    _client1.GetHashCode().ToString()
            //};
        }
        
    }
    
}