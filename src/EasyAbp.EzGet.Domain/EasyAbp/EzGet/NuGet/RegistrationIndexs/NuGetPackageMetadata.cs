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
        public string Summary { get; }
        public string Title { get; }
        public IReadOnlyList<string> Tags { get; }

        public IReadOnlyList<DependencyGroupItem> DependencyGroups { get; }
        public PackageDeprecation Deprecation { get; }

        public NuGetPackageMetadata(
            string catalogUrl,
            string packageName,
            string version,
            string authors,
            string description,
            string language,
            string iconUrl,
            string licenseUrl,
            string projectUrl,
            string packageContentUrl,
            bool? listed,
            string minClientVersion,
            DateTimeOffset published,
            bool requireLicenseAcceptance,
            string summary,
            string title,
            IReadOnlyList<string> tags,
            IReadOnlyList<DependencyGroupItem> dependencyGroups,
            PackageDeprecation deprecation = null)
        {
            CatalogUrl = catalogUrl;
            PackageName = packageName;
            Version = version;
            Authors = authors;
            Description = description;
            Language = language;
            IconUrl = iconUrl;
            LicenseUrl = licenseUrl;
            ProjectUrl = projectUrl;
            PackageContentUrl = packageContentUrl;
            Listed = listed;
            MinClientVersion = minClientVersion;
            Published = published;
            RequireLicenseAcceptance = requireLicenseAcceptance;
            Summary = summary;
            Title = title;
            Tags = tags;
            DependencyGroups = dependencyGroups;
            Deprecation = deprecation;
        }
    }

    public class DependencyGroupItem
    {
        public string TargetFramework { get; }
        public IReadOnlyList<DependencyItem> Dependencies { get; }

        public DependencyGroupItem(
            string targetFramework,
            IReadOnlyList<DependencyItem> dependencyItems)
        {
            TargetFramework = targetFramework;
            Dependencies = dependencyItems;
        }
    }

    public class DependencyItem
    {
        public string DependencyPackageName { get; }
        public string Range { get; }

        public DependencyItem(string dependencyPackageName, string range)
        {
            DependencyPackageName = dependencyPackageName;
            Range = range;
        }
    }

    public class PackageDeprecation
    {
        public IReadOnlyList<string> Reasons { get; }
        public string Message { get; }
        public AlternatePackage AlternatePackage { get; }

        public PackageDeprecation(string message, IReadOnlyList<string> reasons, AlternatePackage alternatePackage)
        {
            Message = message;
            Reasons = reasons;
            AlternatePackage = alternatePackage;
        }
    }

    public class AlternatePackage
    {
        public string PackageName { get; }
        public string Range { get; }

        public AlternatePackage(string packageName, string range)
        {
            PackageName = packageName;
            Range = range;
        }
    }
}
