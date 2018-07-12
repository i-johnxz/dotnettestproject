using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using httpclientfactorysample.Models;
using Refit;

namespace httpclientfactorysample.ClientService
{
    [Headers("X-API-KEY: fbixxxx")]
    public interface IHelloClient
    {
        [Get("/helloworld")]
        Task<Reply> GetMessageAsync();
    }
}
