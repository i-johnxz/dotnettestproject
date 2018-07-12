using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using httpclientfactorysample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace httpclientfactorysample.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        public Reply Get()
        {
            return new Reply()
            {
                Message = "Hello World!"
            };
        }
    }
}