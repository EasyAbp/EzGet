using EasyAbp.EzGet.Credentials;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.Feeds
{
    public class FeedCacheItem
    {
        public Guid Id { get; set; }
        public string FeedName { get; set; }
        public FeedTypeEnum FeedType { get; set; }
        public List<string> CredentialValues { get; set; }
    }
}
