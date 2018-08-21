using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TokenTestDemo.Requests
{
    public class GetAccessTokenRequest
    {
        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; set; }

        [JsonProperty(PropertyName = "client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty(PropertyName = "grant_type")]
        public string GrantType => "client_credentials";
    }
}
