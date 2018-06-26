using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PollyDemos.OutputHelpers;

namespace PollyDemos.Async
{
    public abstract class AsyncDemo : DemoBase
    {
        //protected AsyncDemo(IConfiguration configuration) 
        //    : base(configuration)
        //{
        //}

        //public abstract Task ExecuteAsync(CancellationToken cancellationToken, IProgress<DemoProgress> progress);
    }
}
