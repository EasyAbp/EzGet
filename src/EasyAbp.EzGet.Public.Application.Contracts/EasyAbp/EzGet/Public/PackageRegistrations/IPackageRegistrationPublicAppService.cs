using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet.Public.PackageRegistrations
{
    public interface IPackageRegistrationPublicAppService : IApplicationService
    {
        Task<PackageRegistrationDto> GetAsync(Guid id);
        Task<PagedResultDto<PackageRegistrationDto>> GetListAsync(GetPackageRegistrationsInput input);
    }
}
