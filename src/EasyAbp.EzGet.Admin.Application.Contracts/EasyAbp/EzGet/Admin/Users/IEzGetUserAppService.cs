using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet.Admin.Users
{
    public interface IEzGetUserAppService : IApplicationService
    {
        Task<EzGetUserDto> GetAsync(Guid id);
        Task<PagedResultDto<EzGetUserDto>> GetListAsync(GetEzGetUsersInput input);
    }
}
