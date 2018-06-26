using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace testajax.Controllers
{
    //[Area("brokers")]
    [Route("brokers/[controller]")]
    //[Route("brokers/[controller]/[action]")]
    public class TestController : Controller
    {
        [HttpGet]
        public string GetTest()
        {
            return "service--a1";
        }
        
    }
}