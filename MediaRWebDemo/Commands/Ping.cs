using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace MediaRWebDemo.Commands
{
    public class Ping: IRequest<string>
    {
    }
}
