using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.NuGet.RegistrationIndexs
{
    //See: https://docs.microsoft.com/en-us/nuget/api/registration-base-url-resource#catalog-entry
    public class NuGetPackageMetadata
    {
        public string CatalogUrl { get; }
        public string PackageName { get; }
        public string Version { get; }
        public string Authors { get; }
        public string Description { get; }
        public string Language { get; }
        public string IconUrl { get; }
        public string LicenseUrl { get; }
        public string ProjectUrl { get; }
        public string PackageContentUrl { get; }
        public bool? Listed { get; }
        public string MinClientVersion { get; }
        public DateTimeOffset Published { get; }
        public bool RequireLicenseAcceptance { get; }
        public string Summary { get; set; }
        public IReadOnlyList<string> Tags { get; }
        public string Title { get; }

        public IReadOnlyList<DependencyGroupItem> DependencyGroups { get; set; }
        public PackageDeprecation Deprecation { get; set; }
    }

    public class DependencyGroupItem
    {
        public string TargetFramework { get; }
        public IReadOnlyList<DependencyItem> Dependencies { get; set; }
    }

    public class DependencyItem
    {
        public string DependencyPackageName { get; }
        public string Range { get; }
    }

    public class PackageDeprecation
    {
        public IReadOnlyList<string> Reasons { get; set; }
        public string Message { get; set; }
        public AlternatePackage AlternatePackage { get; set; }
    }

    public class AlternatePackage
    {
        public string PackageName { get; set; }
        public string Range { get; set; }
    }
}
