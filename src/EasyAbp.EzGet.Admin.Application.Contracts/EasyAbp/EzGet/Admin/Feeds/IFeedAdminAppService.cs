using EasyAbp.EzGet.Feeds;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet.Admin.Feeds
{
    public interface IFeedAdminAppService : IApplicationService
    {
        Task<FeedDto> GetAsync(Guid id);
        Task<FeedDto> GetByNameAsync(string name);
        Task<PagedResultDto<FeedDto>> GetListAsync(GetFeedsInput input);
        Task<FeedDto> CreateAsync(CreateFeedAdminDto input);
        Task<FeedDto> UpdateAsync(Guid id, UpdateFeedAdminDto input);
        Task DeleteAsync(Guid id);
    }
}
