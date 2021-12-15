using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EasyAbp.EzGet.Public.NuGet.RegistrationIndexs
{
    public class RegistrationIndexDto
    {
        [JsonPropertyName("@id")]
        [JsonProperty("@id")]
        public string RegistrationIndexUrl { get; set; }

        [JsonPropertyName("@type")]
        [JsonProperty("@type")]
        public IReadOnlyList<string> Types { get; set; }

        public int Count { get; set; }

        [JsonPropertyName("items")]
        [JsonProperty("items")]
        public IReadOnlyList<RegistrationPageDto> Pages { get; set; }
    }
}
