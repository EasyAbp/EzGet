using JetBrains.Annotations;
using NuGet.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EzGet.NuGetPackages
{
    public class PackageManager : DomainService, INuGetPackageManager
    {
        protected INuGetPackageRepository PackageRepository { get; }

        public PackageManager(INuGetPackageRepository packageRepository)
        {
            PackageRepository = packageRepository;
        }

        public virtual async Task<NuGetPackage> CreateAsync([NotNull]PackageArchiveReader packageReader)
        {
            Check.NotNull(packageReader, nameof(packageReader));

            var nuspec = packageReader.NuspecReader;
            (var repositoryUri, var repositoryType) = GetRepositoryMetadata(nuspec);

            var packageName = nuspec.GetId();
            var version = nuspec.GetVersion();

            if (await PackageRepository.ExistsAsync(new UniqueNuGetPackageSpecification(packageName, version.ToNormalizedString())))
            {
                throw new BusinessException(
                    EzGetErrorCodes.PackageAlreadyExisted,
                    "This package is already existed!",
                    $"PackageName:{packageName}, Version:{version.ToNormalizedString()}");
            }

            var package = new NuGetPackage(
                GuidGenerator.Create(),
                nuspec.GetId(),
                nuspec.GetVersion(),
                ParseAuthors(nuspec.GetAuthors()),
                nuspec.GetDescription(),
                packageReader.HasReadme(),
                packageReader.HasEmbeddedIcon(),
                nuspec.GetVersion().IsPrerelease,
                nuspec.GetReleaseNotes(),
                nuspec.GetLanguage(),
                nuspec.GetMinClientVersion().ToNormalizedString(),
                DateTime.UtcNow,
                nuspec.GetRequireLicenseAcceptance(),
                GetSemVerLevel(nuspec),
                nuspec.GetSummary(),
                nuspec.GetTitle(),
                ParseUri(nuspec.GetIconUrl()),
                ParseUri(nuspec.GetLicenseUrl()),
                ParseUri(nuspec.GetProjectUrl()),
                repositoryUri,
                repositoryType,
                ParseTags(nuspec.GetTags()));

            package.AddPackageTypes(nuspec);
            package.AddTargetFrameworks(packageReader);
            package.AddDependencies(nuspec);
            return package;
        }

        private (Uri repositoryUrl, string repositoryType) GetRepositoryMetadata(NuspecReader nuspec)
        {
            var repository = nuspec.GetRepositoryMetadata();

            if (string.IsNullOrEmpty(repository?.Url) ||
                !Uri.TryCreate(repository.Url, UriKind.Absolute, out var repositoryUri))
            {
                return (null, null);
            }

            if (repositoryUri.Scheme != Uri.UriSchemeHttps)
            {
                return (null, null);
            }

            if (repository.Type.Length > 100)
            {
                throw new InvalidOperationException("Repository type must be less than or equal 100 characters");
            }

            return (repositoryUri, repository.Type);
        }

        private static string[] ParseAuthors(string authors)
        {
            if (string.IsNullOrEmpty(authors))
                return new string[0];

            return authors.Split(new[] { ',', ';', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static string[] ParseTags(string tags)
        {
            if (string.IsNullOrEmpty(tags)) return new string[0];

            return tags.Split(new[] { ',', ';', ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static Uri ParseUri(string uriString)
        {
            if (string.IsNullOrEmpty(uriString)) return null;

            return new Uri(uriString);
        }

        private SemVerLevelEnum GetSemVerLevel(NuspecReader nuspec)
        {
            if (nuspec.GetVersion().IsSemVer2)
            {
                return SemVerLevelEnum.SemVer2;
            }

            foreach (var dependencyGroup in nuspec.GetDependencyGroups())
            {
                foreach (var dependency in dependencyGroup.Packages)
                {
                    if ((dependency.VersionRange.MinVersion != null && dependency.VersionRange.MinVersion.IsSemVer2)
                        || (dependency.VersionRange.MaxVersion != null && dependency.VersionRange.MaxVersion.IsSemVer2))
                    {
                        return SemVerLevelEnum.SemVer2;
                    }
                }
            }

            return SemVerLevelEnum.Unknown;
        }
    }
}
