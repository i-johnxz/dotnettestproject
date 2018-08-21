using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TokenWebTest.Models;

namespace TokenWebTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OpenApiController : ControllerBase
    {
        [HttpPost("access_token")]
        public Authenticate Token(GetAccessTokenRequest request)
        {
            //return request.ClientId + request.ClientSecret + request.GrantType;
            return new Authenticate
            {
                AccessToken = "AccessToken",
                ExpiresIn = 7200,
                Openeid = "Openeid",
                RefreshToken = "RefreshToken",
                Username = "Username"
            };
        }

        [HttpPost("message/chaApply")]
        public ChaApplyResult ChaApply(ChaApplyRequest request)
        {
            return new ChaApplyResult();
        }
    }
}