using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace TokenTestDemo.Services
{
    public interface IWangLianAuthenticateApi
    {
        [Get("/openapi/access_token")]
        Task<Authenticate> GetAccessTokenAsync(GetAccessTokenRequest request);
    }
}
