using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EasyAbp.EzGet.Public.NuGet.Packages
{
    public class NuGetPackageSearchResultDto
    {
        [JsonPropertyName("id")]
        [JsonProperty("id")]
        public string PackageName { get; set; }

        public string Version { get; set; }

        public string Description { get; set; }

        public IReadOnlyList<string> Authors { get; set; }

        public string IconUrl { get; set; }

        public string LicenseUrl { get; set; }

        //public IReadOnlyList<SearchResultPackageTypeDto> PackageTypes { get; set; }

        public string ProjectUrl { get; set; }

        [JsonPropertyName("registration")]
        [JsonProperty("registration")]
        public string RegistrationIndexUrl { get; set; }

        public string Summary { get; set; }

        public IReadOnlyList<string> Tags { get; set; }

        public string Title { get; set; }

        public long TotalDownloads { get; set; }

        public IReadOnlyList<SearchResultVersionDto> Versions { get; set; }
    }

    public class SearchResultPackageTypeDto
    {
        public string Name { get; set; }
    }

    public class SearchResultVersionDto
    {
        [JsonPropertyName("@id")]
        [JsonProperty("@id")]
        public string RegistrationLeafUrl { get; set; }

        public string Version { get; set; }

        public long Downloads { get; set; }
    }
}
