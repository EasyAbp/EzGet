using EasyAbp.EzGet.NuGet.Packages;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet.Admin.NuGet.Packages
{
    public interface INuGetPackageAdminAppService : IApplicationService
    {
        Task<NuGetPackageDto> GetAsync(Guid id);
        Task<NuGetPackageDto> GetAsync(string packageName, string version, string feedName);
        Task<byte[]> GetPackageContentAsync(Guid id);
        Task<byte[]> GetPackageManifestAsync(Guid id);
        Task<PagedResultDto<NuGetPackageDto>> GetListAsync(GetNuGetPackagesInput input);
        Task<NuGetPackageDto> CreateAsync(CreateNuGetPackageInputWithStream input);
        Task<NuGetPackageDto> UpdateAsync(Guid id, UpdateNuGetPackageDto input);
        Task UnlistAsync(Guid id);
        Task RelistAsync(Guid id);
    }
}
