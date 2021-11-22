using EasyAbp.EzGet.Admin.Permissions;
using EasyAbp.EzGet.Credentials;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Admin.Credentials
{
    [Authorize(EzGetAdminPermissions.Credentials.Default)]
    public class CredentialAdminAppService : EzGetAdminAppServiceBase, ICredentialAdminAppService
    {
        protected ICredentialRepository CredentialRepository { get; }

        public CredentialAdminAppService(ICredentialRepository credentialRepository)
        {
            CredentialRepository = credentialRepository;
        }

        public virtual async Task<CredentialDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Credential, CredentialDto>(await CredentialRepository.GetAsync(id));
        }
    }
}
