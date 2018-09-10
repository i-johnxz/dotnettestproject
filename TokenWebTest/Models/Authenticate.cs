using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TokenWebTest.Models
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
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }
    }
}
