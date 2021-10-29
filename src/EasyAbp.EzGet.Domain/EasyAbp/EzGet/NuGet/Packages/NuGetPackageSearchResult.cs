using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.NuGet.Packages
{
    public class NuGetPackageSearchResult
    {
        public string PackageName { get; }
        public string Version { get; }
        public string Description { get; }
        public IReadOnlyList<string> Authors { get; }
        public string IconUrl { get; }
        public string LicenseUrl { get; }
        public IReadOnlyList<SearchResultPackageType> PackageTypes { get; }
        public string ProjectUrl { get; }
        public string RegistrationIndexUrl { get; }
        public string Summary { get; }
        public IReadOnlyList<string> Tags { get; }
        public string Title { get; }
        public long TotalDownloads { get; }
        public IReadOnlyList<SearchResultVersion> Versions { get; }

        public NuGetPackageSearchResult(
            string packageName,
            string version,
            string description,
            IReadOnlyList<string> authors,
            string iconUrl,
            string licenseUrl,
            string projectUrl,
            string registrationIndexUrl,
            string summary,
            IReadOnlyList<string> tags,
            long totalDownloads,
            IReadOnlyList<SearchResultPackageType> packageTypes,
            IReadOnlyList<SearchResultVersion> versions)
        {
            PackageName = packageName;
            Version = version;
            Description = description;
            Authors = authors;
            IconUrl = iconUrl;
            LicenseUrl = licenseUrl;
            PackageTypes = packageTypes;
            ProjectUrl = projectUrl;
            RegistrationIndexUrl = registrationIndexUrl;
            Summary = summary;
            Tags = tags;
            TotalDownloads = totalDownloads;
            Versions = versions;
        }
    }

    public class SearchResultPackageType
    {
        public string Name { get; }

        public SearchResultPackageType(string name)
        {
            Name = name;
        }
    }

    public class SearchResultVersion
    {
        public string RegistrationLeafUrl { get; }
        public string Version { get; }
        public long Downloads { get; }

        public SearchResultVersion(string registrationLeafUrl, string version, long downloads)
        {
            RegistrationLeafUrl = registrationLeafUrl;
            Version = version;
            Downloads = downloads;
        }
    }
}
