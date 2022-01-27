using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet.Public.PackageRegistrations
{
    public interface IPackageRegistrationPublicAppService : IApplicationService
    {
        Task<PackageRegistrationDto> GetAsync(Guid id);
        Task<PagedResultDto<PackageRegistrationDto>> GetListAsync(GetPackageRegistrationsInput input);
        Task AddOwnerAsync(Guid id, AddOwnerDto input);
        Task RemoveOwnerAsync(Guid id, Guid userId);
        Task DeleteAsync(Guid id, DeletePackageRegistrationInput input);
    }
}
