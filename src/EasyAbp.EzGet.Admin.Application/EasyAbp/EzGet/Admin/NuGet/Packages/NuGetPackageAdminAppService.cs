using EasyAbp.EzGet.Admin.Permissions;
using EasyAbp.EzGet.NuGet;
using EasyAbp.EzGet.NuGet.Packages;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EzGet.Admin.NuGet.Packages
{
    [Authorize(EzGetAdminPermissions.NuGetPackages.Default)]
    public class NuGetPackageAdminAppService : EzGetAdminAppServiceBase, INuGetPackageAdminAppService
    {
        protected INuGetPackageManager NuGetPackageManager { get; }
        protected INuGetPackageRepository NuGetPackageRepository { get; }
        protected IBlobContainer<NuGetContainer> BlobContainer { get; }

        public NuGetPackageAdminAppService(
            INuGetPackageManager nuGetPackageManager,
            INuGetPackageRepository nuGetPackageRepository,
            IBlobContainer<NuGetContainer> blobContainer)
        {
            NuGetPackageManager = nuGetPackageManager;
            NuGetPackageRepository = nuGetPackageRepository;
            BlobContainer = blobContainer;
        }

        public virtual async Task<NuGetPackageDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<NuGetPackage, NuGetPackageDto>(await NuGetPackageRepository.GetAsync(id));
        }

        public virtual async Task<NuGetPackageDto> GetAsync(string packageName, string version, string feedName)
        {
            var package = await NuGetPackageRepository.GetAsync(
                await NuGetPackageManager.GetUniqueListedSpecification(packageName, version, feedName));
            return ObjectMapper.Map<NuGetPackage, NuGetPackageDto>(package);
        }

        public virtual async Task<byte[]> GetPackageContentAsync(Guid id)
        {
            var package = await NuGetPackageRepository.GetAsync(id);

            if (package == null)
            {
                throw new EntityNotFoundException(typeof(NuGetPackage));
            }

            return await BlobContainer.GetAllBytesOrNullAsync(await NuGetPackageManager.GetNupkgBlobNameAsync(package));
        }

        public virtual async Task<byte[]> GetPackageManifestAsync(Guid id)
        {
            var package = await NuGetPackageRepository.GetAsync(id);

            if (package == null)
            {
                throw new EntityNotFoundException(typeof(NuGetPackage));
            }

            return await BlobContainer.GetAllBytesOrNullAsync(await NuGetPackageManager.GetNuspecBlobNameAsync(package));
        }

        public virtual async Task<PagedResultDto<NuGetPackageDto>> GetListAsync(GetNuGetPackagesInput input)
        {
            input.Filter = input.Filter?.ToLower();
            input.PackageName = input.PackageName?.ToLower();
            input.Version = input.Version?.ToLower();

            var totalCount = await NuGetPackageRepository.GetCountAsync(
                input.Filter,
                input.FeedId,
                input.PackageName,
                input.Version);

            var list = await NuGetPackageRepository.GetListAsync(
                input.Filter,
                input.FeedId,
                input.PackageName,
                input.Version,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount,
                false);

            return new PagedResultDto<NuGetPackageDto>(
                totalCount,
                ObjectMapper.Map<List<NuGetPackage>, List<NuGetPackageDto>>(list));
        }

        [Authorize(EzGetAdminPermissions.NuGetPackages.Update)]
        public virtual async Task UnlistAsync(Guid id)
        {
            var package = await NuGetPackageRepository.GetAsync(id, false);

            if (!package.Listed)
            {
                throw new BusinessException(EzGetErrorCodes.PackageAlreadyUnlisted);
            }

            package.Listed = false;
            await NuGetPackageRepository.UpdateAsync(package);
        }

        [Authorize(EzGetAdminPermissions.NuGetPackages.Update)]
        public virtual async Task RelistAsync(Guid id)
        {
            var package = await NuGetPackageRepository.GetAsync(id, false);

            if (package.Listed)
            {
                throw new BusinessException(EzGetErrorCodes.PackageAlreadyListed);
            }

            package.Listed = true;
            await NuGetPackageRepository.UpdateAsync(package);
        }
    }
}
