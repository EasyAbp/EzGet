using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EzGet.Feeds
{
    public class FeedPermissionGrant : BasicAggregateRoot
    {
        public Guid FeedId { get; }
        public string ProviderName { get; protected set; }
        public string ProviderKey { get; protected internal set; }

        private FeedPermissionGrant()
        {
        }

        public FeedPermissionGrant(Guid feedId, [NotNull] string providerName, [NotNull] string providerKey)
        {
            Check.NotNullOrWhiteSpace(providerName, nameof(providerName));
            Check.NotNullOrWhiteSpace(providerKey, nameof(providerKey));

            FeedId = feedId;
            ProviderName = providerName;
            ProviderKey = providerKey;
        }

        public override object[] GetKeys()
        {
            return new object[] { FeedId, ProviderName, ProviderKey };
        }
    }
}
