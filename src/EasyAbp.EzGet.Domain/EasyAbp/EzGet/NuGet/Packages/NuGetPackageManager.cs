using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using NuGet.Packaging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.BlobStoring;
using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.PackageRegistrations;

namespace EasyAbp.EzGet.NuGet.Packages
{
    public class NuGetPackageManager : DomainService, INuGetPackageManager
    {
        protected INuGetPackageRepository PackageRepository { get; }
        protected IOptions<PacakgeBlobNameOptions> Options { get; }
        protected IFeedStore FeedStore { get; }
        protected IPackageRegistrationRepository PackageRegistrationRepository { get; }
        protected IPackageRegistrationManager PackageRegistrationManager { get; }

        public NuGetPackageManager(
            INuGetPackageRepository packageRepository,
            IOptions<PacakgeBlobNameOptions> options,
            IFeedStore feedStore,
            IPackageRegistrationRepository packageRegistrationRepository,
            IPackageRegistrationManager packageRegistrationManager)
        {
            PackageRepository = packageRepository;
            Options = options;
            FeedStore = feedStore;
            PackageRegistrationRepository = packageRegistrationRepository;
            PackageRegistrationManager = packageRegistrationManager;
        }

        public virtual async Task<NuGetPackage> CreateAsync(
            [NotNull] PackageArchiveReader packageReader,
            long size,
            string feedName = null)
        {
            Check.NotNull(packageReader, nameof(packageReader));

            var nuspec = packageReader.NuspecReader;
            var (repositoryUri, repositoryType) = GetRepositoryMetadata(nuspec);

            var packageName = nuspec.GetId();
            var version = nuspec.GetVersion();
            var feedId = await GetFeedIdByNameAsync(feedName);

            if (await PackageRepository.ExistsAsync(packageName, version.ToNormalizedString(), feedId, null))
            {
                throw new BusinessException(
                    EzGetErrorCodes.PackageAlreadyExisted,
                    "This package is already existed!",
                    $"PackageName:{packageName}, Version:{version.ToNormalizedString()}");
            }

            var packageRegistration = await PackageRegistrationManager.CreateOrUpdateAsync(
                packageName,
                PackageRegistrationPackageTypeConsts.NuGet,
                feedId,
                version.ToNormalizedString().ToLowerInvariant(),
                size,
                nuspec.GetDescription());

            var package = new NuGetPackage(
                GuidGenerator.Create(),
                packageRegistration.Id,
                packageName,
                version,
                ParseAuthors(nuspec.GetAuthors()),
                nuspec.GetDescription(),
                packageReader.HasReadme(),
                packageReader.HasEmbeddedIcon(),
                nuspec.GetVersion().IsPrerelease,
                nuspec.GetReleaseNotes(),
                nuspec.GetLanguage(),
                nuspec.GetMinClientVersion()?.ToNormalizedString(),
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
                ParseTags(nuspec.GetTags()),
                size);

            package.AddPackageTypes(nuspec, GuidGenerator);
            package.AddTargetFrameworks(packageReader, GuidGenerator);
            package.AddDependencies(nuspec, GuidGenerator);
            return package;
        }

        #region blob
        public virtual async Task<string> GetNupkgBlobNameAsync([NotNull] NuGetPackage package)
        {
            Check.NotNull(package, nameof(package));
            return $"{await GetBlobNameAsync(package)}.{NuGetDomainConsts.NupkgFileSuffix}";
        }

        public virtual async Task<string> GetNuspecBlobNameAsync([NotNull] NuGetPackage package)
        {
            Check.NotNull(package, nameof(package));
            return $"{await GetBlobNameAsync(package)}.{NuGetDomainConsts.NuspecFileSuffix}";
        }

        public virtual Task<string> GetReadmeBlobNameAsync([NotNull] NuGetPackage package)
        {
            Check.NotNull(package, nameof(package));
            var separator = Options.Value.BlobNameSeparator;
            return Task.FromResult($"{package.PackageName}{separator}{package.NormalizedVersion}{separator}{NuGetDomainConsts.ReadmeFileName}");
        }

        public virtual Task<string> GetIconBlobNameAsync([NotNull] NuGetPackage package)
        {
            Check.NotNull(package, nameof(package));
            var separator = Options.Value.BlobNameSeparator;
            return Task.FromResult($"{package.PackageName}{separator}{package.NormalizedVersion}{separator}{NuGetDomainConsts.IconFileName}");
        }

        protected virtual Task<string> GetBlobNameAsync(NuGetPackage package)
        {
            var separator = Options.Value.BlobNameSeparator;
            return Task.FromResult($"{package.PackageName}{separator}{package.NormalizedVersion}{separator}{package.PackageName}.{package.NormalizedVersion}");
        }
        #endregion

        #region deletion
        public virtual async Task DeleteLatestAsync(PackageRegistration packageRegistration)
        {
            var list = await PackageRepository.GetListByPackageNameAndFeedIdAsync(
                packageRegistration.PackageName,
                null,
                null,
                packageRegistration.FeedId,
                false);

            if (list.Count <= 0)
            {
                await PackageRegistrationRepository.DeleteAsync(packageRegistration);
                return;
            }

            list.Sort((item1, item2) => string.Compare(item1.NormalizedVersion, item2.NormalizedVersion, StringComparison.Ordinal));
            var latestNuGetPackage = list.Last();

            await PackageRepository.DeleteAsync(latestNuGetPackage);

            if (list.Count <= 1)
            {
                await PackageRegistrationRepository.DeleteAsync(packageRegistration);
            }
            else
            {
                var secondBottom = list.Skip(list.Count - 2).Take(1).First();
                packageRegistration.SetLastVersion(secondBottom.NormalizedVersion);
                packageRegistration.Size = secondBottom.Size;
                packageRegistration.Description = secondBottom.Description;

                await PackageRegistrationRepository.UpdateAsync(packageRegistration);
            }
        }

        public virtual async Task DeleteAllButLatestAsync(PackageRegistration packageRegistration)
        {
            var list = await PackageRepository.GetListByPackageNameAndFeedIdAsync(
                packageRegistration.PackageName,
                null,
                null,
                packageRegistration.FeedId,
                false);

            if (list.Count <= 0)
            {
                await PackageRegistrationRepository.DeleteAsync(packageRegistration);
                return;
            }

            if (list.Count <= 1)
            {
                return;
            }

            list.Sort((item1, item2) => item1.NormalizedVersion.CompareTo(item2.NormalizedVersion));
            var removeList = list.Take(list.Count - 1).ToList();
            await PackageRepository.DeleteManyAsync(removeList);
        }

        public virtual async Task DeleteAllAsync(PackageRegistration packageRegistration)
        {
            var list = await PackageRepository.GetListByPackageNameAndFeedIdAsync(
                packageRegistration.PackageName,
                null,
                null,
                packageRegistration.FeedId,
                false);

            await PackageRegistrationRepository.DeleteAsync(packageRegistration);
            await PackageRepository.DeleteManyAsync(list);
        }
        #endregion

        #region private methods

        private async Task<Guid?> GetFeedIdByNameAsync(string feedName)
        {
            Guid? feedId = null;

            if (!string.IsNullOrEmpty(feedName))
            {
                feedId = (await FeedStore.GetAsync(feedName)).Id;
            }

            return feedId;
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
        #endregion
    }
}
