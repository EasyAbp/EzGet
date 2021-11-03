using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EasyAbp.EzGet.Public.NuGet.ServiceIndexs
{
    public class ServiceIndexResourceDto
    {
        [JsonProperty("@id")]
        [JsonPropertyName("@id")]
        public string ResourceUrl { get; set; }

        [JsonProperty("@type")]
        [JsonPropertyName("@type")]
        public string Type { get; set; }

        [JsonProperty("comment")]
        [JsonPropertyName("comment")]
        public string Comment { get; set; }
    }
}
