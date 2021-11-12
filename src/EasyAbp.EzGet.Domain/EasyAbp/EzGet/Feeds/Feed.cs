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
        public Guid UserId { get; private set; }
        public string FeedName { get; }
        public string Description { get; set; }
        public FeedTypeEnum FeedType { get; set; }
        public Guid? TenantId { get; }
        public virtual ICollection<FeedCredential> FeedCredentials { get; }

        private Feed()
        {
            FeedCredentials = new List<FeedCredential>();
        }

        internal Feed(
            Guid id,
            Guid userId,
            [NotNull] string feedName,
            FeedTypeEnum feedType,
            string description = null,
            Guid? tenantId = null)
            : this()
        {
            Id = id;
            UserId = userId;
            FeedName = Check.NotNullOrWhiteSpace(feedName, nameof(feedName));
            FeedType = feedType;
            Description = description;
            TenantId = tenantId;
        }

        internal void AddCredentialId(Guid credentialId)
        {
            FeedCredentials.Add(new FeedCredential(Id, credentialId));
        }

        internal void SetUserId(Guid userId)
        {
            UserId = userId;
            FeedCredentials.Clear();
        }
    }
}
