using EasyAbp.EzGet.Feeds;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Public.Feeds
{
    [Area(EzGetPublicRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = EzGetPublicRemoteServiceConsts.RemoteServiceName)]
    [Route("api/ez-get-public/feeds")]
    public class FeedPublicController : EzGetPublicControllerBase, IFeedPublicAppService
    {
        protected IFeedPublicAppService FeedPublicAppService { get; }

        public FeedPublicController(IFeedPublicAppService feedPublicAppService)
        {
            FeedPublicAppService = feedPublicAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<FeedDto> GetAsync(Guid id)
        {
            return FeedPublicAppService.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<FeedDto>> GetListAsync(GetFeedsInput input)
        {
            return FeedPublicAppService.GetListAsync(input);
        }

        [HttpPost]
        public virtual Task<FeedDto> CreateAsync(CreateFeedPublicDto input)
        {
            return FeedPublicAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<FeedDto> UpdateAsync(Guid id, UpdateFeedPublicDto input)
        {
            return FeedPublicAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return FeedPublicAppService.DeleteAsync(id);
        }
    }
}
