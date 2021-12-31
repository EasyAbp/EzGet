using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EasyAbp.EzGet.Public.NuGet.RegistrationIndexs
{
    public class RegistrationPageItemDto
    {
        [JsonPropertyName("@id")]
        public string RegistrationLeafUrl { get; set; }

        [JsonPropertyName("packageContent")]
        public string PackageContentUrl { get; set; }

        [JsonPropertyName("catalogEntry")]
        public NuGetPackageMetadataDto PackageMetadata { get; set; }
    }
}
