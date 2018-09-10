using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TokenTestDemo.Requests
{
    public class BaseRequest
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "openid")]
        public string Openid { get; set; }
    }
}
