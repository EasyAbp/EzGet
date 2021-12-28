using EasyAbp.EzGet.Public.NuGet.Packages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EasyAbp.EzGet.Public.Models
{
    public class PackageSearchModel
    {
        [JsonPropertyName("@context")]
        [JsonProperty("@context")]
        public SearchContext Context { get; set; }

        [JsonPropertyName("totalHits")]
        [JsonProperty("totalHits")]
        public long Count { get; set; }

        [JsonPropertyName("data")]
        [JsonProperty("data")]
        public IEnumerable<NuGetPackageSearchResultDto> Packages { get; set; }

        public class SearchContext
        {
            [JsonPropertyName("@vocab")]
            [JsonProperty("@vocab")]

            public string Vocab { get; set; }

            [JsonPropertyName("@base")]
            [JsonProperty("@base")]
            public string Base { get; set; }

            public static SearchContext Default(string registrationBaseUrl)
            {
                return new SearchContext
                {
                    Vocab = "http://schema.nuget.org/schema#",
                    Base = registrationBaseUrl
                };
            }
        }
    }
}
