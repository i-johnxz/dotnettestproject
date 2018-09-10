using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediaRASPNETCOREDemo.Models
{
    public class Handler2: INotificationHandler<SomeEvent>
    {
        private readonly ILogger<Handler2> _logger;

        public Handler2(ILogger<Handler2> logger)
        {
            _logger = logger;
        }

        public Task Handle(SomeEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogWarning($"Handled: {notification.Message}");
            return Task.CompletedTask;
        }
    }
}
