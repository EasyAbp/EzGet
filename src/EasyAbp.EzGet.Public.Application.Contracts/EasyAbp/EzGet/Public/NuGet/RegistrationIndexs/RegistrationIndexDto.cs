using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EasyAbp.EzGet.Public.NuGet.RegistrationIndexs
{
    public class RegistrationIndexDto
    {
        [JsonPropertyName("@id")]
        public string RegistrationIndexUrl { get; set; }

        [JsonPropertyName("@type")]
        public IReadOnlyList<string> Types { get; set; }

        public int Count { get; set; }

        [JsonPropertyName("items")]
        public IReadOnlyList<RegistrationPageDto> Pages { get; set; }
    }
}
