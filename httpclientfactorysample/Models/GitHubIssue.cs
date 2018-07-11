using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace httpclientfactorysample.Models
{
    public class GitHubIssue
    {
        [JsonProperty(PropertyName = "html_url")]
        public string Url { get; set; }

        public string Title { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime Created { get; set; }
    }
}
