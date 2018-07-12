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
        private readonly IHelloClient _client;

        public ValuesController(IHelloClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<Reply> Get()
        {
            var message = await _client.GetMessageAsync();
            return message;
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