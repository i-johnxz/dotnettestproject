using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestCircuitBreaker.Services;

namespace TestCircuitBreaker.Controllers
{
    //[Produces("application/json")]
    [Route("api/Values")]
    public class ValuesController : Controller
    {
        private readonly IAService _aService;

        public ValuesController(IAService aService)
        {
            _aService = aService;
        }

        [HttpGet]
        public Task<string> Get()
        {
            return _aService.GetAsync();
        }
    }
}