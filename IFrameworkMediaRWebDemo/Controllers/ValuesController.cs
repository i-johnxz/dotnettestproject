using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IFrameworkMediaRWebDemo.Models;
using IFrameworkMediaRWebDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IFrameworkMediaRWebDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IDemoService _demoService;

        public ValuesController(ILogger<ValuesController> logger, 
                                IDemoService demoService)
        {
            _logger = logger;
            _demoService = demoService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            _logger.LogDebug("test");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public Task<PostResult> Post([FromBody] CreatePostModel request)
        {
            return _demoService.PostResult(request);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
