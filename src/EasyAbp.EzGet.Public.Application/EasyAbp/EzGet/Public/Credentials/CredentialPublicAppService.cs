using EasyAbp.EzGet.Credentials;
using EasyAbp.EzGet.Public.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Users;

namespace EasyAbp.EzGet.Public.Credentials
{
    [Authorize(EzGetPublicPermissions.Credentials.Default)]
    public class CredentialPublicAppService : EzGetPublicAppServiceBase, ICredentialPublicAppService
    {
        protected ICredentialRepository CredentialRepository { get; }

        public CredentialPublicAppService(ICredentialRepository credentialRepository)
        {
            CredentialRepository = credentialRepository;
        }

        public virtual async Task<CredentialDto> GetAsync(Guid id)
        {
            var credential = await CredentialRepository.GetAsync(id);
            CheckUser(credential.UserId, EzGetErrorCodes.NoAuthorizeHandleThisCredential);

            return ObjectMapper.Map<Credential, CredentialDto>(credential);
        }

        public virtual async Task<PagedResultDto<CredentialDto>> GetListAsync(GetCredentialsInput input)
        {
            var list = await CredentialRepository.GetListAsync(CurrentUser.GetId(), input.Sorting, input.MaxResultCount, input.SkipCount);
            var totalCount = await CredentialRepository.GetCountAsync(CurrentUser.GetId());

            return new PagedResultDto<CredentialDto>(
                totalCount,
                ObjectMapper.Map<List<Credential>, List<CredentialDto>>(list));
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
        public virtual async Task<CredentialDto> UpdateAsync(Guid id, UpdateCredentialDto input)
        {
            var credential = await CredentialRepository.GetAsync(id);
            CheckUser(credential.UserId, EzGetErrorCodes.NoAuthorizeHandleThisCredential);

            credential.Description = input.Description;

            return ObjectMapper.Map<Credential, CredentialDto>(await CredentialRepository.UpdateAsync(credential));
        }

        [Authorize(EzGetPublicPermissions.Credentials.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var credential = await CredentialRepository.GetAsync(id);
            CheckUser(credential.UserId, EzGetErrorCodes.NoAuthorizeHandleThisCredential);

            await CredentialRepository.DeleteAsync(credential);
        }
    }
}
