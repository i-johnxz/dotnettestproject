using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MediaRASPNETCOREDemo.Models
{
    public class Ping: IRequest<string>
    {

    }

    public class PingHandler : IRequestHandler<Ping, string>
    {
        public Task<string> Handle(Ping request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Pong");
        }
    }

    public class Ping2Handler: IRequestHandler<Ping, string>
    {
        public Task<string> Handle(Ping request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Pong2");
        }
    }
}
