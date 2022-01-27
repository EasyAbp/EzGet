using EasyAbp.EzGet.NuGet.Packages;
using EasyAbp.EzGet.PackageRegistrations;
using EasyAbp.EzGet.Packages;
using EasyAbp.EzGet.Public.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Public.PackageRegistrations
{
    [Authorize(EzGetPublicPermissions.PackageRegistrations.Default)]
    public class PackageRegistrationPublicAppService : EzGetPublicAppServiceBase, IPackageRegistrationPublicAppService
    {
        protected IPackageRegistrationRepository PackageRegistrationRepository { get; }

        public PackageRegistrationPublicAppService(
            IPackageRegistrationRepository packageRegistrationRepository)
        {
            PackageRegistrationRepository = packageRegistrationRepository;
        }

        public virtual async Task<PackageRegistrationDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<PackageRegistration, PackageRegistrationDto>(
                await PackageRegistrationRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<PackageRegistrationDto>> GetListAsync(GetPackageRegistrationsInput input)
        {
            var count = await PackageRegistrationRepository.GetCountAsync(input.Filter, input.FeedId, CurrentUser.Id);
            var list = await PackageRegistrationRepository.GetListAsync(input.Filter, input.FeedId, CurrentUser.Id, input.MaxResultCount, input.SkipCount, input.Sorting);
            
            return new PagedResultDto<PackageRegistrationDto>(
                count,
                ObjectMapper.Map<List<PackageRegistration>, List<PackageRegistrationDto>>(list));
        }
        
        public virtual async Task AddOwnerAsync(Guid id, AddOwnerDto input)
        {
            var packageRegistration = await PackageRegistrationRepository.GetAsync(id);
            packageRegistration.AddOwnerId(input.UserId, input.OwnerType);
            await PackageRegistrationRepository.UpdateAsync(packageRegistration);
        }

        public virtual async Task RemoveOwnerAsync(Guid id, Guid userId)
        {
            var packageRegistration = await PackageRegistrationRepository.GetAsync(id);
            packageRegistration.RemoveOwnerId(userId);
            await PackageRegistrationRepository.UpdateAsync(packageRegistration);
        }

        public virtual async Task DeleteAsync(Guid id, DeletePackageRegistrationInput input)
        {
            IPackageManager packageManager;
            var packageRegistration = await PackageRegistrationRepository.GetAsync(id);

            switch (packageRegistration.PackageType)
            {
                case PackageRegistrationPackageTypeConsts.NuGet:
                    packageManager = LazyServiceProvider.LazyGetRequiredService<INuGetPackageManager>();
                    break;

                default:
                    throw new InvalidOperationException($"Unknown type. PackageType:{packageRegistration.PackageType}");
            }

            switch (input.Type)
            {
                case PackagesDeletionTypeEnum.Latest:
                    await packageManager.DeleteLatestAsync(packageRegistration);
                    break;

                case PackagesDeletionTypeEnum.AllButLatest:
                    await packageManager.DeleteAllButLatestAsync(packageRegistration);
                    break;

                case PackagesDeletionTypeEnum.All:
                    await packageManager.DeleteAllAsync(packageRegistration);
                    break;
            }
        }
    }
}
