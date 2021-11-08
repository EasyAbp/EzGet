using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet.Public.Credentials
{
    public interface ICredentialPublicAppService : IApplicationService
    {
        Task<CredentialDto> GetAsync(Guid id);
        Task<CredentialDto> CreateAsync(CreateCredentialDto input);
        Task<CredentialDto> UpdateAsync(Guid id, UpdateCredentialDto inpu);
        Task DeleteAsync(Guid id);
    }
}
