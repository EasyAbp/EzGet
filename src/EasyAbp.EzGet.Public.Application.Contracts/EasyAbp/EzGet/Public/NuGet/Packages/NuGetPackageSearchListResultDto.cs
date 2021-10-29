using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EasyAbp.EzGet.Public.NuGet.Packages
{
    public class NuGetPackageSearchListResultDto
    {
        [JsonPropertyName("totalHits")]
        [JsonProperty("totalHits")]
        public long Count { get; set; }

        [JsonPropertyName("data")]
        [JsonProperty("data")]
        public List<NuGetPackageSearchResultDto> Packages { get; set; }
    }
}
