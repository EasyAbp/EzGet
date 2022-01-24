using EasyAbp.EzGet.PackageRegistrations;
using EasyAbp.EzGet.Public.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
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
            var count = await PackageRegistrationRepository.GetCountAsync(input.Filter, input.FeedId);
            var list = await PackageRegistrationRepository.GetListAsync(input.Filter, input.FeedId, input.MaxResultCount, input.SkipCount, input.Sorting);
            
            return new PagedResultDto<PackageRegistrationDto>(
                count,
                ObjectMapper.Map<List<PackageRegistration>, List<PackageRegistrationDto>>(list));
        }
    }
}
