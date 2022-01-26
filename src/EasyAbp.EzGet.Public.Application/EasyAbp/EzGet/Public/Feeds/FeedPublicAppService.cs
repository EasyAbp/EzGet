using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.Public.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EzGet.Public.Feeds
{
    [Authorize(EzGetPublicPermissions.Feeds.Default)]
    public class FeedPublicAppService : EzGetPublicAppServiceBase, IFeedPublicAppService
    {
        protected IFeedRepository FeedRepository { get; }
        protected IFeedManager FeedManager { get; }

        public FeedPublicAppService(
            IFeedRepository feedRepository,
            IFeedManager feedManager)
        {
            FeedRepository = feedRepository;
            FeedManager = feedManager;
        }

        public virtual async Task<FeedDto> GetAsync(Guid id)
        {
            var feed = await FeedRepository.GetAsync(id);
            CheckUser(feed.UserId, EzGetErrorCodes.NoAuthorizeHandleThisFeed);

            return ObjectMapper.Map<Feed, FeedDto>(feed);
        }

        public virtual async Task<FeedDto> FindByNameAsync(string name)
        {
            var feed = await FeedRepository.FindByNameAsync(name);

            if (null == feed)
            {
                return null;
            }

            CheckUser(feed.UserId, EzGetErrorCodes.NoAuthorizeHandleThisFeed);

            return ObjectMapper.Map<Feed, FeedDto>(feed);
        }

        public virtual async Task<PagedResultDto<FeedDto>> GetListAsync(GetFeedsInput input)
        {
            var list = await FeedRepository.GetListAsync(
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount,
                input.Filter,
                input.FeedName,
                CurrentUser.Id);

            var totalCount = await FeedRepository.GetCountAsync(input.Filter);

            return new PagedResultDto<FeedDto>(
                totalCount,
                ObjectMapper.Map<List<Feed>, List<FeedDto>>(list));
        }

        [Authorize(EzGetPublicPermissions.Feeds.Create)]
        public virtual async Task<FeedDto> CreateAsync(CreateFeedPublicDto input)
        {
            var feed = await FeedManager.CreateAsync((Guid)CurrentUser.Id, input.FeedName, input.FeedType, input.Description);

            foreach (var item in input.CredentialIds)
            {
                await FeedManager.AddCredentialAsync(feed, item);
            }

            return ObjectMapper.Map<Feed, FeedDto>(await FeedRepository.InsertAsync(feed));
        }

        [Authorize(EzGetPublicPermissions.Feeds.Update)]
        public virtual async Task<FeedDto> UpdateAsync(Guid id, UpdateFeedPublicDto input)
        {
            var feed = await FeedRepository.GetAsync(id);
            CheckUser(feed.UserId, EzGetErrorCodes.NoAuthorizeHandleThisFeed);

            feed.Description = input.Description;

            feed.Description = input.Description;
            feed.FeedType = input.FeedType;

            foreach (var item in input.CredentialIds)
            {
                await FeedManager.AddCredentialAsync(feed, item);
            }

            return ObjectMapper.Map<Feed, FeedDto>(await FeedRepository.UpdateAsync(feed));
        }

        [Authorize(EzGetPublicPermissions.Feeds.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var feed = await FeedRepository.GetAsync(id);
            CheckUser(feed.UserId, EzGetErrorCodes.NoAuthorizeHandleThisFeed);

            await FeedRepository.DeleteAsync(feed);
        }
    }
}
