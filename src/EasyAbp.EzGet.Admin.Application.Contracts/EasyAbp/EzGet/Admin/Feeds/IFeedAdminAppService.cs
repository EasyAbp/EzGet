using EasyAbp.EzGet.Feeds;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet.Admin.Feeds
{
    public interface IFeedAdminAppService : IApplicationService
    {
        Task<FeedDto> GetAsync(Guid id);
        Task<FeedDto> CreateAsync(CreateFeedAdminDto input);
    }
}
