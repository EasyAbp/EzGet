using EasyAbp.EzGet.Credentials;
using EasyAbp.EzGet.Public.Permissions;
using EasyAbp.EzGet.Users;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Users;

namespace EasyAbp.EzGet.Public.Credentials
{
    [Authorize(EzGetPublicPermissions.Credentials.Default)]
    public class CredentialPublicAppService : EzGetPublicAppServiceBase, ICredentialPublicAppService
    {
        protected ICredentialRepository CredentialRepository { get; }

        public CredentialPublicAppService(
            ICredentialRepository credentialRepository)
        {
            CredentialRepository = credentialRepository;
        }

        public virtual async Task<CredentialDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Credential, CredentialDto>(await CredentialRepository.GetAsync(id));
        }

        [Authorize(EzGetPublicPermissions.Credentials.Create)]
        public virtual async Task<CredentialDto> CreateAsync(CreateCredentialDto input)
        {
            var credential = new Credential(
                GuidGenerator.Create(),
                CurrentUser.GetId(),
                Guid.NewGuid().ToString(),
                input.Expiration,
                input.GlobPattern,
                input.Description);

            foreach (var scope in input.Scopes)
            {
                credential.AddScope(scope);
            }

            return ObjectMapper.Map<Credential, CredentialDto>(await CredentialRepository.InsertAsync(credential));
        }

        [Authorize(EzGetPublicPermissions.Credentials.Update)]
        public virtual async Task<CredentialDto> UpdateAsync(Guid id, UpdateCredentialDto inpu)
        {
            var credential = await CredentialRepository.GetAsync(id);
            credential.Description = inpu.Description;
            return ObjectMapper.Map<Credential, CredentialDto>(await CredentialRepository.UpdateAsync(credential));
        }

        [Authorize(EzGetPublicPermissions.Credentials.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await CredentialRepository.DeleteAsync(id);
        }
    }
}
