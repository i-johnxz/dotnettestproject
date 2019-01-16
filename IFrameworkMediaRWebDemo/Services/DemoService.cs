using System.Threading.Tasks;
using IFrameworkMediaRWebDemo.Commands;
using IFrameworkMediaRWebDemo.Models;
using MediatR;

namespace IFrameworkMediaRWebDemo.Services
{
    public class DemoService : IDemoService
    {
        private readonly IMediator _mediator;

        public DemoService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<bool> CreatePost(CreatePostModel createPostModel)
        {
            return Task.FromResult(true);
        }

        public Task<PostResult> PostResult(CreatePostModel createPostModel)
        {
            return _mediator.Send(new CreatePostRequest()
            {
                Title = createPostModel.Title,
                Content = createPostModel.Content
            });
        }
    }
}