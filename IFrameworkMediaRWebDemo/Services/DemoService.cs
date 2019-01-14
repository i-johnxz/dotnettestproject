using System.Threading.Tasks;
using IFrameworkMediaRWebDemo.Models;

namespace IFrameworkMediaRWebDemo.Services
{
    public class DemoService : IDemoService
    {
        public Task<bool> CreatePost(CreatePostModel createPostModel)
        {
            return Task.FromResult(true);
        }
    }
}