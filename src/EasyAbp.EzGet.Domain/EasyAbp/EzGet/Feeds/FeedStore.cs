using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EzGet.Feeds
{
    public class FeedStore : IFeedStore, ITransientDependency
    {
        protected IFeedRepository FeedRepository { get; }
        protected IDistributedCache<FeedCacheItem> Cache { get; }

        public FeedStore(
            IFeedRepository feedRepository,
            IDistributedCache<FeedCacheItem> cache)
        {
            FeedRepository = feedRepository;
            Cache = cache;
        }

        public virtual async Task<FeedCacheItem> GetAsync([NotNull] string feedName)
        {
            Check.NotNullOrWhiteSpace(feedName, nameof(feedName));

            return await Cache.GetOrAddAsync(feedName, async () =>
            {
                var feed = await FeedRepository.FindByNameAsync(feedName);

                if (null == feed)
                {
                    return null;
                }

                var feedCahceItem = new FeedCacheItem
                {
                    Id = feed.Id,
                    FeedName = feed.FeedName,
                    FeedType = feed.FeedType,
                };

                return feedCahceItem;
            });
        }
    }
}
