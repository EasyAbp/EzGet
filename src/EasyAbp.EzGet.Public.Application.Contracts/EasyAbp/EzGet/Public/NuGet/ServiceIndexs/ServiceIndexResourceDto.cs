using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EasyAbp.EzGet.Public.NuGet.ServiceIndexs
{
    public class ServiceIndexResourceDto
    {
        [JsonPropertyName("@id")]
        public string ResourceUrl { get; set; }

        [JsonPropertyName("@type")]
        public string Type { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }
    }
}
