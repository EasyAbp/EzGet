using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Public.Credentials
{
    [RemoteService(Name = EzGetPublicRemoteServiceConsts.RemoteServiceName)]
    [Area("ezget-public")]
    [Route("api/ezget-public/credentials")]
    public class CredentialPublicController : EzGetPublicControllerBase, ICredentialPublicAppService
    {
        protected ICredentialPublicAppService CredentialPublicAppService { get; }

        public CredentialPublicController(ICredentialPublicAppService credentialPublicAppService)
        {
            CredentialPublicAppService = credentialPublicAppService;
        }

        [HttpPost]
        public virtual Task<CredentialDto> CreateAsync(CreateCredentialDto input)
        {
            return CredentialPublicAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return CredentialPublicAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CredentialDto> GetAsync(Guid id)
        {
            return CredentialPublicAppService.GetAsync(id);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CredentialDto> UpdateAsync(Guid id, UpdateCredentialDto inpu)
        {
            return CredentialPublicAppService.UpdateAsync(id, inpu);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<CredentialDto>> GetListAsync(GetCredentialsInput input)
        {
            return CredentialPublicAppService.GetListAsync(input);
        }
    }
}
