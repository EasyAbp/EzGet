using JetBrains.Annotations;
using NuGet.Packaging;
using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EzGet.NuGetPackages
{
    public class NuGetPackage : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public string PackageName { get; }
        public string[] Authors { get; }
        public string Description { get; set; }
        public long Downloads { get; private set; }
        public bool HasReadme { get; private set; }
        public bool HasEmbeddedIcon { get; private set; }
        public bool IsPrerelease { get; }
        public string ReleaseNotes { get; }
        public string Language { get; set; }
        public bool Listed { get; set; }
        public string MinClientVersion { get; set; }
        public DateTime Published { get; set; }
        public bool RequireLicenseAcceptance { get; set; }
        public SemVerLevelEnum SemVerLevel { get; }
        public string Summary { get; set; }
        public string Title { get; set; }
        public Uri IconUrl { get; set; }
        public Uri LicenseUrl { get; set; }
        public Uri ProjectUrl { get; set; }
        public Uri RepositoryUrl { get; set; }
        public string RepositoryType { get; set; }
        public string[] Tags { get; set; }
        public string NormalizedVersion { get; private set; }
        public string OriginalVersion { get; private set; }
        public Guid? TenantId { get; }

        public virtual ICollection<PackageDependency> Dependencies { get; }
        public virtual ICollection<PackageType> PackageTypes { get; }
        public virtual ICollection<TargetFramework> TargetFrameworks { get; }

        private NuGetPackage()
        {
            Dependencies = new List<PackageDependency>();
            PackageTypes = new List<PackageType>();
            TargetFrameworks = new List<TargetFramework>();
        }

        internal NuGetPackage(
            Guid id,
            string packageName,
            NuGetVersion nuGetVersion,
            string[] authors,
            string description,
            bool hasReadme,
            bool hasEmbeddedIcon,
            bool isPrerelease,
            string releaseNotes,
            string language,
            string minClientVersion,
            DateTime published,
            bool requireLicenseAcceptance,
            SemVerLevelEnum semVerLevel,
            string summary,
            string title,
            Uri iconUrl,
            Uri licenseUrl,
            Uri projectUrl,
            Uri repositoryUrl,
            string repositoryType,
            string[] tags,
            Guid? tenantId = null)
            : this()
        {
            Id = id;
            PackageName = Check.NotNullOrWhiteSpace(packageName, nameof(packageName));
            Authors = authors ?? new string[0];
            Description = description;
            HasReadme = hasReadme;
            HasEmbeddedIcon = hasEmbeddedIcon;
            IsPrerelease = isPrerelease;
            ReleaseNotes = releaseNotes;
            Language = language;
            MinClientVersion = minClientVersion;
            Published = published;
            RequireLicenseAcceptance = requireLicenseAcceptance;
            SemVerLevel = semVerLevel;
            Summary = summary;
            Title = title;
            IconUrl = iconUrl;
            LicenseUrl = licenseUrl;
            ProjectUrl = projectUrl;
            RepositoryUrl = repositoryUrl;
            RepositoryType = repositoryType;
            Tags = tags ?? new string[0];
            TenantId = tenantId;
            Listed = true;
            Downloads = 0;
            SetNuGetVersion(nuGetVersion);
        }

        public NuGetVersion GetNuGetVersion()
        {
            return NuGetVersion.Parse(OriginalVersion != null ? OriginalVersion : NormalizedVersion);
        }

        public void SetNuGetVersion([NotNull] NuGetVersion nuGetVersion)
        {
            Check.NotNull(nuGetVersion, nameof(nuGetVersion));
            NormalizedVersion = nuGetVersion.ToNormalizedString().ToLowerInvariant();
            OriginalVersion = nuGetVersion.OriginalVersion;
        }

        public void AddPackageTypes(NuspecReader nuspec)
        {
            var packageTypes = nuspec
                .GetPackageTypes()
                .Select(t => new PackageType(this, t.Name, t.Version.ToString()))
                .ToList();

            if (packageTypes.Count == 0)
            {
                packageTypes.Add(
                    new PackageType(
                        this,
                        NuGet.Packaging.Core.PackageType.Dependency.Name,
                        NuGet.Packaging.Core.PackageType.Dependency.Version.ToString()));
            }

            PackageTypes.AddRange(packageTypes);
        }

        public void AddDependencies(NuspecReader nuspec)
        {
            var dependencies = new List<PackageDependency>();

            foreach (var group in nuspec.GetDependencyGroups())
            {
                var targetFramework = group.TargetFramework.GetShortFolderName();

                if (!group.Packages.Any())
                {
                    dependencies.Add(new PackageDependency(this, null, null, targetFramework));
                }

                foreach (var dependency in group.Packages)
                {
                    dependencies.Add(new PackageDependency(this, dependency.Id, dependency.VersionRange?.ToString(), targetFramework));
                }
            }

            Dependencies.AddRange(dependencies);
        }

        public void AddTargetFrameworks(PackageArchiveReader packageReader)
        {
            var targetFrameworks = packageReader
                .GetSupportedFrameworks()
                .Select(f => new TargetFramework(this, f.GetShortFolderName()))
                .ToList();

            if (targetFrameworks.Count == 0)
            {
                targetFrameworks.Add(new TargetFramework(this, "any"));
            }

            TargetFrameworks.AddRange(targetFrameworks);
        }
    }
}
