﻿using EasyAbp.EzGet.NuGet.Packages;
using NuGet.Packaging;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Specifications;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.BlobStoring;
using EasyAbp.EzGet.NuGet;
using Microsoft.AspNetCore.Authorization;
using EasyAbp.EzGet.Public.Permissions;
using Volo.Abp.Users;
using EasyAbp.EzGet.Feeds;

namespace EasyAbp.EzGet.Public.NuGet.Packages
{
    [Authorize]
    public class NuGetPackagePublicAppService : EzGetPublicNuGetAppServiceBase, INuGetPackagePublicAppService
    {
        protected INuGetPackageManager NuGetPackageManager { get; }
        protected INuGetPackageRepository NuGetPackageRepository { get; }
        protected IBlobContainer<NuGetContainer> BlobContainer { get; }
        protected INuGetPackageSearcher NuGetPackageSearcher { get; }

        public NuGetPackagePublicAppService(
            INuGetPackageManager nuGetPackageManager,
            INuGetPackageRepository nuGetPackageRepository,
            IBlobContainer<NuGetContainer> blobContainer,
            INuGetPackageSearcher nuGetPackageSearcher,
            IFeedStore feedStore) : base(feedStore)
        {
            NuGetPackageManager = nuGetPackageManager;
            NuGetPackageRepository = nuGetPackageRepository;
            BlobContainer = blobContainer;
            NuGetPackageSearcher = nuGetPackageSearcher;
        }

        [AllowAnonymousIfFeedPublic]
        [Authorize(EzGetPublicPermissions.NuGetPackages.Default)]
        public virtual async Task<NuGetPackageSearchListResultDto> SearchListAsync(SearcherListInput input)
        {
            return ObjectMapper.Map<NuGetPackageSearchListResult, NuGetPackageSearchListResultDto>(
                await NuGetPackageSearcher.SearchListAsync(
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
                await NuGetPackageManager.GetUniqueListedSpecification(packageName, version, feedName));

            if (package == null)
                return null;

            return ObjectMapper.Map<NuGetPackage, NuGetPackageDto>(package);
        }

        [AllowAnonymousIfFeedPublic]
        [Authorize(EzGetPublicPermissions.NuGetPackages.Default)]
        public virtual async Task<byte[]> GetPackageContentAsync(string packageName, string version, string feedName)
        {
            var package = await NuGetPackageRepository.GetAsync(
                await NuGetPackageManager.GetUniqueListedSpecification(packageName, version, feedName));

            if (package == null)
                return null;

            return await BlobContainer.GetAllBytesOrNullAsync(await NuGetPackageManager.GetNupkgBlobNameAsync(package));
        }

        [AllowAnonymousIfFeedPublic]
        [Authorize(EzGetPublicPermissions.NuGetPackages.Default)]
        public virtual async Task<byte[]> GetPackageManifestAsync(string packageName, string version, string feedName)
        {
            var package = await NuGetPackageRepository.GetAsync(
                await NuGetPackageManager.GetUniqueListedSpecification(packageName, version, feedName));

            if (package == null)
                return null;

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
                await NuGetPackageManager.GetUniqueListedSpecification(packageName, version, feedName),
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
                await NuGetPackageManager.GetUniqueListedSpecification(packageName, version, feedName),
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
        public virtual async Task<List<string>> GetVersionListByPackageName(string packageName, string feedName)
        {
            var packages = await NuGetPackageRepository.GetListByPackageNameAndFeedIdAsync(
                packageName,
                await GetFeedIdAsync(feedName),
                false);

            return packages.Select(p => p.NormalizedVersion).ToList();
        }
    }
}
