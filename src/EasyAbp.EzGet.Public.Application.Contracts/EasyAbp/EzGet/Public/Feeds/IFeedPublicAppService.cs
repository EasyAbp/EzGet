using EasyAbp.EzGet.Feeds;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet.Public.Feeds
{
    public interface IFeedPublicAppService : IApplicationService
    {
        Task<FeedDto> GetAsync(Guid id);
        Task<FeedDto> FindByNameAsync(string name);
        Task<PagedResultDto<FeedDto>> GetListAsync(GetFeedsInput input);
        Task<FeedDto> CreateAsync(CreateFeedPublicDto input);
        Task<FeedDto> UpdateAsync(Guid id, UpdateFeedPublicDto input);
        Task DeleteAsync(Guid id);
    }
}
