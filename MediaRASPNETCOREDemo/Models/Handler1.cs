using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediaRASPNETCOREDemo.Models
{
    public class Handler1 : INotificationHandler<SomeEvent>
    {
        private readonly ILogger<Handler1> _logger;

        public Handler1(ILogger<Handler1> logger)
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
