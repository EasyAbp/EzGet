using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EasyAbp.EzGet.Public.NuGet.Packages
{
    public class NuGetPackageSearchNameListResultDto
    {
        [JsonPropertyName("totalHits")]
        [JsonProperty("totalHits")]
        public long Count { get; set; }

        [JsonPropertyName("data")]
        [JsonProperty("data")]
        public IEnumerable<string> Names { get; set; }
    }
}
