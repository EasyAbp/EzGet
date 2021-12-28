using EasyAbp.EzGet.NuGet.Packages;
using NuGet.Packaging;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.BlobStoring;
using EasyAbp.EzGet.NuGet;
using Microsoft.AspNetCore.Authorization;
using EasyAbp.EzGet.Public.Permissions;
using EasyAbp.EzGet.Feeds;
using Volo.Abp.Domain.Entities;
using EasyAbp.EzGet.NuGet.ServiceIndexs;

namespace EasyAbp.EzGet.Public.NuGet.Packages
{
    public class NuGetPackagePublicAppService : EzGetPublicNuGetAppServiceBase, INuGetPackagePublicAppService
    {
        protected INuGetPackageManager NuGetPackageManager { get; }
        protected INuGetPackageRepository NuGetPackageRepository { get; }
        protected IBlobContainer<NuGetContainer> BlobContainer { get; }
        protected INuGetPackageSearcher NuGetPackageSearcher { get; }
        protected IServiceIndexUrlGenerator ServiceIndexUrlGenerator { get; }

        public NuGetPackagePublicAppService(
            INuGetPackageManager nuGetPackageManager,
            INuGetPackageRepository nuGetPackageRepository,
            IBlobContainer<NuGetContainer> blobContainer,
            INuGetPackageSearcher nuGetPackageSearcher,
            IFeedStore feedStore,
            IServiceIndexUrlGenerator serviceIndexUrlGenerator)
            : base(feedStore)
        {
            NuGetPackageManager = nuGetPackageManager;
            NuGetPackageRepository = nuGetPackageRepository;
            BlobContainer = blobContainer;
            NuGetPackageSearcher = nuGetPackageSearcher;
            ServiceIndexUrlGenerator = serviceIndexUrlGenerator;
        }

        [AllowAnonymousIfFeedPublic]
        [Authorize(EzGetPublicPermissions.NuGetPackages.Default)]
        public virtual async Task<NuGetPackageSearchPackageListResultDto> SearchPackageListAsync(SearchPackageListInput input)
        {
            var result = ObjectMapper.Map<NuGetPackageSearchPackageListResult, NuGetPackageSearchPackageListResultDto>(
                await NuGetPackageSearcher.SearchPackageListAsync(
                    input.SkipCount,
                    input.MaxResultCount,
                    input.Filter,
                    input.IncludePrerelease,
                    input.IncludeSemVer2,
                    input.PackageType,
                    input.FeedName));

            return result;
        }

        [AllowAnonymousIfFeedPublic]
        [Authorize(EzGetPublicPermissions.NuGetPackages.Default)]
        public virtual async Task<NuGetPackageSearchNameListResultDto> SearchNameListAsync(SearchNameListInput input)
        {
            return ObjectMapper.Map<NuGetPackageSearchNameListResult, NuGetPackageSearchNameListResultDto>(
                await NuGetPackageSearcher.SearchNameListAsync(
                    input.SkipCount,
                    input.MaxResultCount,
                    input.Filter,
                    input.IncludePrerelease,
                    input.IncludeSemVer2,
                    input.PackageType,
                    input.FeedName));
        }

        [AllowAnonymousIfFeedPublic]
        [Authorize(EzGetPublicPermissions.NuGetPackages.Default)]
        public virtual async Task<NuGetPackageDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<NuGetPackage, NuGetPackageDto>(await NuGetPackageRepository.GetAsync(id));
        }

        [AllowAnonymousIfFeedPublic]
        [Authorize(EzGetPublicPermissions.NuGetPackages.Default)]
        public virtual async Task<NuGetPackageDto> GetAsync(string packageName, string version, string feedName)
        {
            var package = await NuGetPackageRepository.GetAsync(
                packageName,
                version,
                await GetFeedIdAsync(feedName),
                true);

            if (package == null)
            {
                throw new EntityNotFoundException(typeof(NuGetPackage));
            }

            return ObjectMapper.Map<NuGetPackage, NuGetPackageDto>(package);
        }

        [AllowAnonymousIfFeedPublic]
        [Authorize(EzGetPublicPermissions.NuGetPackages.Default)]
        public virtual async Task<byte[]> GetPackageContentAsync(string packageName, string version, string feedName)
        {
            var package = await NuGetPackageRepository.GetAsync(
                packageName,
                version,
                await GetFeedIdAsync(feedName),
                true);

            if (package == null)
            {
                throw new EntityNotFoundException(typeof(NuGetPackage));
            }

            return await BlobContainer.GetAllBytesOrNullAsync(await NuGetPackageManager.GetNupkgBlobNameAsync(package));
        }

        [AllowAnonymousIfFeedPublic]
        [Authorize(EzGetPublicPermissions.NuGetPackages.Default)]
        public virtual async Task<byte[]> GetPackageManifestAsync(string packageName, string version, string feedName)
        {
            var package = await NuGetPackageRepository.GetAsync(
                packageName,
                version,
                await GetFeedIdAsync(feedName),
                true);

            if (package == null)
            {
                throw new EntityNotFoundException(typeof(NuGetPackage));
            }

            return await BlobContainer.GetAllBytesOrNullAsync(await NuGetPackageManager.GetNuspecBlobNameAsync(package));
        }

        [Authorize(EzGetPublicPermissions.NuGetPackages.Create)]
        public virtual async Task<NuGetPackageDto> CreateAsync(CreateNuGetPackageInputWithStream input, string feedName = null)
        {
            using (var packageStream = input.File.GetStream())
            {
                using (var packageReader = new PackageArchiveReader(packageStream, leaveStreamOpen: true))
                {
                    Stream readmeStream = null;
                    Stream iconStream = null;

                    var nuspecStream = await packageReader.GetNuspecAsync(default);
                    var package = await NuGetPackageManager.CreateAsync(packageReader, feedName);
                    await NuGetPackageRepository.InsertAsync(package);

                    if (package.HasReadme)
                    {
                        readmeStream = await packageReader.GetReadmeAsync();
                        await BlobContainer.SaveAsync(await NuGetPackageManager.GetReadmeBlobNameAsync(package), readmeStream);
                    }

                    if (package.HasEmbeddedIcon)
                    {
                        iconStream = await packageReader.GetIconAsync();
                        await BlobContainer.SaveAsync(await NuGetPackageManager.GetIconBlobNameAsync(package), iconStream);
                    }

                    await BlobContainer.SaveAsync(await NuGetPackageManager.GetNupkgBlobNameAsync(package), packageStream);
                    await BlobContainer.SaveAsync(await NuGetPackageManager.GetNuspecBlobNameAsync(package), nuspecStream);

                    return ObjectMapper.Map<NuGetPackage, NuGetPackageDto>(package);
                }
            }
        }

        [Authorize(EzGetPublicPermissions.NuGetPackages.Unlist)]
        public virtual async Task UnlistAsync(string packageName, string version, string feedName)
        {
            var package = await NuGetPackageRepository.GetAsync(
                packageName,
                version,
                await GetFeedIdAsync(feedName),
                true,
                false);

            if (!package.Listed)
            {
                throw new BusinessException(EzGetErrorCodes.PackageAlreadyUnlisted);
            }

            package.Listed = false;
            await NuGetPackageRepository.UpdateAsync(package);
        }

        [Authorize(EzGetPublicPermissions.NuGetPackages.Relist)]
        public virtual async Task RelistAsync(string packageName, string version, string feedName)
        {
            var package = await NuGetPackageRepository.GetAsync(
                packageName,
                version,
                await GetFeedIdAsync(feedName),
                true,
                false);

            if (package.Listed)
            {
                throw new BusinessException(EzGetErrorCodes.PackageAlreadyListed);
            }

            package.Listed = true;
            await NuGetPackageRepository.UpdateAsync(package);
        }

        [AllowAnonymousIfFeedPublic]
        [Authorize(EzGetPublicPermissions.NuGetPackages.Default)]
        public virtual async Task<List<string>> GetVersionListAsync(GetVersionListInput input)
        {
            var packages = await NuGetPackageRepository.GetListByPackageNameAndFeedIdAsync(
                input.PackageName,
                input.IncludePrerelease,
                input.IncludeSemVer2,
                await GetFeedIdAsync(input.FeedName),
                false);

            var result = packages.Select(p => p.NormalizedVersion).ToList();
            result.Sort((item1, item2) => item1.CompareTo(item2));
            return result;
        }
    }
}
