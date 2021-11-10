using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Feeds
{
    public interface IFeedStore
    {
        Task<FeedCacheItem> GetAsync([NotNull] string feedName);
    }
}
