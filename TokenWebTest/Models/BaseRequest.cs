using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TokenWebTest.Models
{
    public class BaseRequest
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "openid")]
        public string Openid { get; set; }

        [JsonProperty(PropertyName = "nonce_str")]
        public string NonceStr { get; set; }

        [JsonProperty(PropertyName = "sign")]
        public string Sign { get; set; }
    }
}
