using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Admin.Users
{
    [RemoteService(Name = EzGetAdminRemoteServiceConsts.RemoteServiceName)]
    [Area("ezget-admin")]
    [Route("api/ezget-admin/users")]
    public class EzGetUserController : EzGetAdminControllerBase, IEzGetUserAppService
    {
        protected IEzGetUserAppService EzGetUserAppService { get; }

        public EzGetUserController(IEzGetUserAppService ezGetUserAppService)
        {
            EzGetUserAppService = ezGetUserAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<EzGetUserDto> GetAsync(Guid id)
        {
            return EzGetUserAppService.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<EzGetUserDto>> GetListAsync(GetEzGetUsersInput input)
        {
            return EzGetUserAppService.GetListAsync(input);
        }
    }
}
