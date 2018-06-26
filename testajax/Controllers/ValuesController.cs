using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace testajax.Controllers
{
    //[Produces("application/json")]


    //[Area("brokers")]


    //[Route("brokers")]
    //[route("","")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        [HttpGet, Route("gettests2")]
        public IdName GetTests2()
        {
            return new IdName("1", "test");
        }

        [HttpPost, Route("add")]
        public string AddTest([FromBody]IdName idName)
        {
            return idName.ToString();
        }

        [HttpGet, Route("check")]
        public string CheckAccount(string account, string password)
        {
            return $"account: {account}, password: {password}";
        }

        [HttpPost, Route("testpost")]
        public void TestPost([FromBody] IdName idName)
        {

        }
    }

    public class IdName
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IdName()
        {
            
        }

        public IdName(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}";
        }
    }

}