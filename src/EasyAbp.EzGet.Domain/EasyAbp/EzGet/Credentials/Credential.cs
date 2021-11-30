using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EzGet.Credentials
{
    public class Credential : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid UserId { get; }
        public string Value { get; }
        public string Description { get; set; }
        public DateTime? Expires { get; set; }
        public string GlobPattern { get; }
        public Guid? TenantId { get; }
        public virtual ICollection<CredentialScope> Scopes { get; }

        private Credential()
        {
            Scopes = new List<CredentialScope>();
        }

        public Credential(
            Guid id,
            Guid userId,
            [NotNull] string value,
            TimeSpan? expiration,
            string globPattern,
            string description = null,
            Guid? tenantId = null)
            : this()
        {
            Id = id;
            UserId = userId;
            Value = Check.NotNullOrWhiteSpace(value, nameof(value));
            GlobPattern = globPattern;
            Description = description;
            TenantId = tenantId;

            if (expiration.HasValue && expiration.Value > TimeSpan.Zero)
            {
                Expires = DateTime.UtcNow.Add(expiration.Value);
            }
        }

        public void AddScope(ScopeAllowActionEnum allowAction)
        {
            Scopes.Add(new CredentialScope(this, allowAction));
        }

        public bool HasExpired()
        {
            if (Expires.HasValue)
            {
                return DateTime.UtcNow >= Expires.Value;
            }

            return false;
        }
    }
}
