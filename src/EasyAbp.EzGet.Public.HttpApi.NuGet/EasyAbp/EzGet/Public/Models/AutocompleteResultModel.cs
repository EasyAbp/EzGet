using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EasyAbp.EzGet.Public.Models
{
    public class AutocompleteResultModel
    {
        [JsonPropertyName("@context")]
        [JsonProperty("@context")]
        public AutocompleteContext Context { get; set; }

        [JsonPropertyName("totalHits")]
        [JsonProperty("totalHits")]
        public long Count { get; set; }

        [JsonPropertyName("data")]
        [JsonProperty("data")]
        public IEnumerable<string> Data { get; set; }

        public class AutocompleteContext
        {
            [JsonPropertyName("@vocab")]
            [JsonProperty("@vocab")]

            public string Vocab { get; set; }

            public static AutocompleteContext Default()
            {
                return new AutocompleteContext
                {
                    Vocab = "http://schema.nuget.org/schema#",
                };
            }
        }
    }
}
