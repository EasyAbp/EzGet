using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EzGet.Feeds
{
    public class Feed : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public string FeedName { get; }
        public string Description { get; set; }
        public FeedTypeEnum FeedType { get; }
        public Guid? TenantId { get; }
        public virtual ICollection<FeedCredential> FeedCredentials { get; }

        private Feed()
        {
            FeedCredentials = new List<FeedCredential>();
        }

        public Feed(
            Guid id,
            [NotNull] string feedName,
            FeedTypeEnum feedType,
            string description = null,
            Guid? tenantId = null)
            : this()
        {
            Id = id;
            FeedName = Check.NotNullOrWhiteSpace(feedName, nameof(feedName));
            FeedType = feedType;
            Description = description;
            TenantId = tenantId;
        }
    }
}
