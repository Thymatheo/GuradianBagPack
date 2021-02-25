using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BungieApiHelper.Entity.Application
{
    public class Application
    {
        [JsonInclude]
        [JsonPropertyName("applicationId")]
        public int id { get; set; }
        [JsonInclude]
        [JsonPropertyName("name")]
        public string name { get; set; }
        [JsonPropertyName("redirectUrl")]
        public string redirectUrl { get; set; }
        [JsonPropertyName("link")]
        public string link { get; set; }
        [JsonPropertyName("scope")]
        public int scope { get; set; }
        [JsonPropertyName("origin")]
        public string origin { get; set; }
        [JsonPropertyName("status")]
        public int status { get; set; }
        [JsonPropertyName("creationTime")]
        public DateTime creationTime { get; set; }
        [JsonPropertyName("statusChanged")]
        public DateTime statusChanged { get; set; }
        [JsonPropertyName("firstPublished")]
        public DateTime firstPublished { get; set; }
        [JsonPropertyName("team")]
        public IEnumerable<ApplicationDeveloper> team { get; set; }
    }
}
