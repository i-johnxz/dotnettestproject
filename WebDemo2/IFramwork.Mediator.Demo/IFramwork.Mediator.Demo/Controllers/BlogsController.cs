using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IFramwork.Mediator.Demo.Commands;
using IFramwork.Mediator.Demo.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IFramwork.Mediator.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public Task<string> Post([FromBody] CreateBlogRequest model)
        {
            return _mediator.Send(model);
        }
    }
}