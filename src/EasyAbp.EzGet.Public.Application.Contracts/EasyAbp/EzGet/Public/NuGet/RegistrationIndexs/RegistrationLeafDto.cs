using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EasyAbp.EzGet.Public.NuGet.RegistrationIndexs
{
    public class RegistrationLeafDto
    {
        [JsonPropertyName("@id")]
        public string RegistrationLeafUrl { get; set; }

        [JsonPropertyName("@type")]
        public IReadOnlyList<string> Types { get; set; }

        public bool Listed { get; set; }

        [JsonPropertyName("packageContent")]
        public string PackageContentUrl { get; set; }

        public DateTimeOffset Published { get; set; }

        [JsonPropertyName("registration")]
        public string RegistrationIndexUrl { get; set; }
    }
}
