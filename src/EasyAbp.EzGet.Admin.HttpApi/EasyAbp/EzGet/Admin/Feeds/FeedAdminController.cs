using EasyAbp.EzGet.Feeds;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Admin.Feeds
{
    [RemoteService(Name = EzGetAdminRemoteServiceConsts.RemoteServiceName)]
    [Area("ezget-admin")]
    [Route("api/ezget-admin/feeds")]
    public class FeedAdminController : EzGetAdminControllerBase, IFeedAdminAppService
    {
        protected IFeedAdminAppService FeedAdminAppService { get; }

        public FeedAdminController(IFeedAdminAppService feedAdminAppService)
        {
            FeedAdminAppService = feedAdminAppService;
        }

        [HttpPost]
        public virtual Task<FeedDto> CreateAsync(CreateFeedAdminDto input)
        {
            return FeedAdminAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return FeedAdminAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<FeedDto> GetAsync(Guid id)
        {
            return FeedAdminAppService.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<FeedDto>> GetListAsync(GetFeedsInput input)
        {
            return FeedAdminAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<FeedDto> UpdateAsync(Guid id, UpdateFeedAdminDto input)
        {
            return FeedAdminAppService.UpdateAsync(id, input);
        }
    }
}
