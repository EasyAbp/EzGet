using EasyAbp.EzGet.Admin.Permissions;
using EasyAbp.EzGet.Credentials;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

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

        public virtual async Task<PagedResultDto<CredentialDto>> GetListAsync(GetCredentialsInput input)
        {
            var list = await CredentialRepository.GetListAsync(
                input.UserId,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount);

            var totalCount = await CredentialRepository.GetCountAsync(input.UserId);

            return new PagedResultDto<CredentialDto>(
                totalCount,
                ObjectMapper.Map<List<Credential>, List<CredentialDto>>(list));
        }

        [Authorize(EzGetAdminPermissions.Credentials.Create)]
        public virtual async Task<CredentialDto> CreateAsync(CreateCredentialDto input)
        {
            var credential = new Credential(
                GuidGenerator.Create(),
                input.UserId,
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

        [Authorize(EzGetAdminPermissions.Credentials.Update)]
        public virtual async Task<CredentialDto> UpdateAsync(Guid id, UpdateCredentialDto input)
        {
            var credential = await CredentialRepository.GetAsync(id);

            credential.ConcurrencyStamp = input.ConcurrencyStamp;
            credential.Description = input.Description;
            credential.Expires = input.Expires;

            var scopeList = credential.Scopes.ToList();

            for (int i = scopeList.Count - 1; i >= 0; i--)
            {
                var scope = scopeList[i];

                if (!input.Scopes.Any(p => p == scope.AllowAction))
                {
                    credential.Scopes.Remove(scope);
                }
            }

            foreach (var scope in input.Scopes)
            {
                if (!scopeList.Any(p => p.AllowAction == scope))
                {
                    credential.AddScope(scope);
                }
            }

            return ObjectMapper.Map<Credential, CredentialDto>(await CredentialRepository.UpdateAsync(credential));
        }

        [Authorize(EzGetAdminPermissions.Credentials.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await CredentialRepository.DeleteAsync(id);
        }
    }
}
