using EasyAbp.EzGet.Admin.Permissions;
using EasyAbp.EzGet.Feeds;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Admin.Feeds
{
    [Authorize(EzGetAdminPermissions.Feeds.Default)]
    public class FeedAdminAppService : EzGetAdminAppServiceBase, IFeedAdminAppService
    {
        protected IFeedRepository FeedRepository { get; }
        protected IFeedManager FeedManager { get; }

        public FeedAdminAppService(
            IFeedRepository feedRepository,
            IFeedManager feedManager)
        {
            FeedRepository = feedRepository;
            FeedManager = feedManager;
        }

        public virtual async Task<FeedDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Feed, FeedDto>(await FeedRepository.GetAsync(id));
        }

        public virtual async Task<FeedDto> CreateAsync(CreateFeedAdminDto input)
        {
            var feed = await FeedManager.CreateAsync(input.UserId, input.FeedName, input.FeedType, input.Description);

            foreach (var item in input.CredentialIds)
            {
                await FeedManager.AddCredentialAsync(feed, item);
            }

            return ObjectMapper.Map<Feed, FeedDto>(await FeedRepository.InsertAsync(feed));
        }

        public virtual async Task<FeedDto> UpdateAsync(Guid id, UpdateFeedAdminDto input)
        {
            var feed = await FeedRepository.GetAsync(id);

            if (feed.UserId != input.UserId)
            {
                await FeedManager.SetUserIdAsync(feed, input.UserId);
            }

            feed.Description = input.Description;
            feed.FeedType = input.FeedType;

            foreach (var item in input.CredentialIds)
            {
                await FeedManager.AddCredentialAsync(feed, item);
            }

            return ObjectMapper.Map<Feed, FeedDto>(await FeedRepository.UpdateAsync(feed));
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await FeedRepository.DeleteAsync(id);
        }
    }
}
