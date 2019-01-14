using System.Threading.Tasks;
using IFramework.Infrastructure;
using IFramwork.Mediator.Demo.Commands;
using IFramwork.Mediator.Demo.Models;

namespace IFramwork.Mediator.Demo.Services
{
    public class BlogService : IBlogService
    {
        public Task<string> CreateAsync(CreateBlogRequest model)
        {
            return Task.FromResult(model.ToJson());
        }
    }
}