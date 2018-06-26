using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiB.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[]
            {
                "value1 from webapi B",
                "value2 from webapi B"
            };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"value {id} from WebApiB";
        }
    }
}