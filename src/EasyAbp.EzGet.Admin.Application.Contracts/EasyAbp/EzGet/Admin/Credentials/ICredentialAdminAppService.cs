using EasyAbp.EzGet.Credentials;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet.Admin.Credentials
{
    public interface ICredentialAdminAppService : IApplicationService
    {
        Task<CredentialDto> GetAsync(Guid id);
        Task<PagedResultDto<CredentialDto>> GetListAsync(GetCredentialsInput input);
        Task<CredentialDto> CreateAsync(CreateCredentialDto input);
        Task<CredentialDto> UpdateAsync(Guid id, UpdateCredentialDto input);
        Task DeleteAsync(Guid id);
    }
}
