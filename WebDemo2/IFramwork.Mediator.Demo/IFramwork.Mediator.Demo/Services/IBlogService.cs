using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IFramwork.Mediator.Demo.Commands;
using IFramwork.Mediator.Demo.Models;

namespace IFramwork.Mediator.Demo.Services
{
    public interface IBlogService
    {
        Task<string> CreateAsync(CreateBlogRequest model);
    }
}
