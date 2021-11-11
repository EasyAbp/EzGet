using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.Localization;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet.Public
{
    public abstract class EzGetPublicNuGetAppServiceBase : EzGetPublicAppServiceBase
    {
        protected IFeedStore FeedStore { get; }

        protected EzGetPublicNuGetAppServiceBase(IFeedStore feedStore) : base()
        {
            FeedStore = feedStore;
        }

        protected virtual async Task<Guid?> GetFeedIdAsync(string feedName)
        {
            Guid? feedId = null;
            if (!string.IsNullOrEmpty(feedName))
            {
                feedId = (await FeedStore.GetAsync(feedName)).Id;
            }

            return feedId;
        }
    }
}
