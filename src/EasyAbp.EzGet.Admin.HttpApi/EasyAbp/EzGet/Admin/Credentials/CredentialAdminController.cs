using EasyAbp.EzGet.Credentials;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Admin.Credentials
{
    [RemoteService(Name = EzGetAdminRemoteServiceConsts.RemoteServiceName)]
    [Area("ezget-admin")]
    [Route("api/ezget-admin/credentials")]
    public class CredentialAdminController : EzGetAdminControllerBase, ICredentialAdminAppService
    {
        protected ICredentialAdminAppService CredentialAdminAppService { get; }

        public CredentialAdminController(ICredentialAdminAppService credentialAdminAppService)
        {
            CredentialAdminAppService = credentialAdminAppService;
        }

        [HttpPost]
        public virtual Task<CredentialDto> CreateAsync(CreateCredentialDto input)
        {
            return CredentialAdminAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return CredentialAdminAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CredentialDto> GetAsync(Guid id)
        {
            return CredentialAdminAppService.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<CredentialDto>> GetListAsync(GetCredentialsInput input)
        {
            return CredentialAdminAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CredentialDto> UpdateAsync(Guid id, UpdateCredentialDto input)
        {
            return CredentialAdminAppService.UpdateAsync(id, input);
        }

        [HttpGet]
        [Route("by-feed-id/{feedId}")]
        public virtual Task<List<CredentialDto>> GetListByFeedIdAsync(Guid feedId)
        {
            return CredentialAdminAppService.GetListByFeedIdAsync(feedId);
        }
    }
}
