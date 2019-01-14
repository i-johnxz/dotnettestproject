using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IFramwork.Mediator.Demo.Services;
using MediatR;

namespace IFramwork.Mediator.Demo.Commands
{
    public class CreateBlogRequestHandler: IRequestHandler<CreateBlogRequest, string>
    {
        private readonly IBlogService _blogService;

        public CreateBlogRequestHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public Task<string> Handle(CreateBlogRequest request, CancellationToken cancellationToken)
        {
            return _blogService.CreateAsync(request);
        }
    }
}
