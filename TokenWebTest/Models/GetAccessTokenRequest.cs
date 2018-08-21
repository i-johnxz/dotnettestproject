using Newtonsoft.Json;

namespace TokenWebTest.Models
{
    public class GetAccessTokenRequest
    {
        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; set; }

        [JsonProperty(PropertyName = "client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty(PropertyName = "grant_type")]
        public string GrantType { get; set; }
    }
}