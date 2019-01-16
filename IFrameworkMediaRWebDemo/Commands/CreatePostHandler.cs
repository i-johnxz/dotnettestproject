using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IFramework.Infrastructure;
using IFrameworkMediaRWebDemo.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IFrameworkMediaRWebDemo.Commands
{
    public class CreatePostHandler : IRequestHandler<CreatePostRequest, PostResult>
    {
        public Task<PostResult> Handle(CreatePostRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new PostResult()
            {
                Result = $"{request.Title}_{request.Content}"
            });
        }
    }
}
