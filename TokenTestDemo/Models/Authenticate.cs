using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TokenTestDemo.Models
{
    public class Authenticate
    {
        [JsonProperty(PropertyName = "access_tocken")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty(PropertyName = "openeid")]
        public string Openeid { get; set; }
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        [JsonProperty(PropertyName = "username")]
        public int ExpiresIn { get; set; }
    }
}
