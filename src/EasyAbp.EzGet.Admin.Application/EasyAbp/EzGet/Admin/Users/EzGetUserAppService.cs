using EasyAbp.EzGet.Admin.Permissions;
using EasyAbp.EzGet.Users;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Admin.Users
{
    [Authorize(EzGetAdminPermissions.Users.Default)]
    public class EzGetUserAppService : EzGetAdminAppServiceBase, IEzGetUserAppService
    {
        protected IEzGetUserRepository EzGetUserRepository { get; }

        public EzGetUserAppService(IEzGetUserRepository ezGetUserRepository)
        {
            EzGetUserRepository = ezGetUserRepository;
        }

        public virtual async Task<EzGetUserDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<EzGetUser, EzGetUserDto>(await EzGetUserRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<EzGetUserDto>> GetListAsync(GetEzGetUsersInput input)
        {
            var list = await EzGetUserRepository.GetListAsync(
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount,
                input.Filter,
                input.userName,
                input.phoneNumber,
                input.emailAddress);

            var totleCount = await EzGetUserRepository.GetCountAsync(
                input.Filter,
                input.userName,
                input.phoneNumber,
                input.emailAddress);

            return new PagedResultDto<EzGetUserDto>(
                totleCount,
                ObjectMapper.Map<List<EzGetUser>, List<EzGetUserDto>>(list));
        }
    }
}
