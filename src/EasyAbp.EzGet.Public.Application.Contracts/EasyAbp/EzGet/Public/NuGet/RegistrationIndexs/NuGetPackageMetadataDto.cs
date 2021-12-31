using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EasyAbp.EzGet.Public.NuGet.RegistrationIndexs
{
    public class NuGetPackageMetadataDto
    {
        [JsonPropertyName("@id")]
        public string CatalogUrl { get; set; }

        [JsonPropertyName("id")]
        public string PackageName { get; set; }

        public string Version { get; set; }
        public string Authors { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string IconUrl { get; set; }
        public string LicenseUrl { get; set; }
        public string ProjectUrl { get; set; }

        [JsonPropertyName("packageContent")]
        public string PackageContentUrl { get; set; }

        public bool? Listed { get; set; }
        public string MinClientVersion { get; set; }
        public DateTimeOffset Published { get; set; }
        public bool RequireLicenseAcceptance { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }
        public IReadOnlyList<string> Tags { get; set; }
        public IReadOnlyList<DependencyGroupItemDto> DependencyGroups { get; set; }
        public PackageDeprecationDto Deprecation { get; set; }
    }

    public class DependencyGroupItemDto
    {
        public string TargetFramework { get; set; }
        public IReadOnlyList<DependencyItemDto> Dependencies { get; set; }
    }

    public class DependencyItemDto
    {
        [JsonPropertyName("id")]
        public string DependencyPackageName { get; set; }

        public string Range { get; set; }
    }

    public class PackageDeprecationDto
    {
        public IReadOnlyList<string> Reasons { get; set; }
        public string Message { get; set; }
        public AlternatePackageDto AlternatePackage { get; set; }
    }

    public class AlternatePackageDto
    {
        [JsonPropertyName("id")]
        public string PackageName { get; set; }
        public string Range { get; set; }
    }
}
