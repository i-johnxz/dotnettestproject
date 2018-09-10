using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TokenWebTest.Models
{
    public class ChaApplyResult: BaseResult
    {
        [JsonProperty(PropertyName = "openid")]
        public string Openid { get; set; }
    }

    public class BaseResult
    {
        [JsonProperty(PropertyName = "returnCode")]
        public string ReturnCode { get; set; }

        [JsonProperty(PropertyName = "errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty(PropertyName = "errorMsg")]
        public string ErrorMsg { get; set; }

        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; set; }

        [JsonProperty(PropertyName = "nonce_str")]
        public string NonceStr { get; set; }

        [JsonProperty(PropertyName = "sign")]
        public string Sign { get; set; }

        public bool Success => ReturnCode == "SUCCESS";
    }
}
