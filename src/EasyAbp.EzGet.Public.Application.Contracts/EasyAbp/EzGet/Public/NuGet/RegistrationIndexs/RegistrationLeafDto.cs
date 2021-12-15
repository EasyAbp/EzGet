using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EasyAbp.EzGet.Public.NuGet.RegistrationIndexs
{
    public class RegistrationLeafDto
    {
        [JsonPropertyName("@id")]
        [JsonProperty("@id")]
        public string RegistrationLeafUrl { get; set; }

        [JsonPropertyName("@type")]
        [JsonProperty("@type")]
        public IReadOnlyList<string> Types { get; set; }

        public bool Listed { get; set; }

        [JsonPropertyName("packageContent")]
        [JsonProperty("packageContent")]
        public string PackageContentUrl { get; set; }

        public DateTimeOffset Published { get; set; }

        [JsonPropertyName("registration")]
        [JsonProperty("registration")]
        public string RegistrationIndexUrl { get; set; }
    }
}
