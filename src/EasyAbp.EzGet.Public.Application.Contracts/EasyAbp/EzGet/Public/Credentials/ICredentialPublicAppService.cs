using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet.Public.Credentials
{
    public interface ICredentialPublicAppService : IApplicationService
    {
        Task<CredentialDto> GetAsync(Guid id);
        Task<PagedResultDto<CredentialDto>> GetListAsync(GetCredentialsInput input);
        Task<CredentialDto> CreateAsync(CreateCredentialDto input);
        Task<CredentialDto> UpdateAsync(Guid id, UpdateCredentialDto inpu);
        Task DeleteAsync(Guid id);
    }
}
