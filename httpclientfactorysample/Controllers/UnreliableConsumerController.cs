using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using httpclientfactorysample.Services;
using Microsoft.AspNetCore.Mvc;

namespace httpclientfactorysample.Controllers
{
    public class UnreliableConsumerController : Controller
    {
        private readonly UnreliableEndpointCallerService _unreliableEndpointCallerService;

        public UnreliableConsumerController(UnreliableEndpointCallerService unreliableEndpointCallerService)
        {
            _unreliableEndpointCallerService = unreliableEndpointCallerService;
        }

        public async Task<IActionResult> Index()
        {
            var status = await _unreliableEndpointCallerService.GetDataFromUnreliableEndpoint("http://localhost:5001/helloworld");

            return Ok(status);
        }
    }
}