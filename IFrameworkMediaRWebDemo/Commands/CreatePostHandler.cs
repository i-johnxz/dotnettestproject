using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IFramework.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IFrameworkMediaRWebDemo.Commands
{
    public class CreatePostHandler: IRequestHandler<CreatePostRequest, bool>
    {
        private readonly ILogger _logger;

        public CreatePostHandler(ILogger<CreatePostHandler> logger)
        {
            _logger = logger;
        }

        public Task<bool> Handle(CreatePostRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug(request.ToJson());
            return Task.FromResult(true);
        }
    }
}
