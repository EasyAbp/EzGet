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
        public string Description { get; }
        public DateTime? Expires { get; set; }
        public Guid? TenantId { get; }
        public virtual ICollection<Scope> Scopes { get; }

        private Credential()
        {
            Scopes = new List<Scope>();
        }

        public Credential(
            Guid id,
            Guid userId,
            [NotNull] string value,
            TimeSpan? expiration,
            string description = null,
            Guid? tenantId = null)
            : this()
        {
            Id = id;
            UserId = userId;
            Value = Check.NotNullOrWhiteSpace(value, nameof(value));
            Description = description;
            TenantId = tenantId;

            if (expiration.HasValue && expiration.Value > TimeSpan.Zero)
            {
                Expires = DateTime.UtcNow.Add(expiration.Value);
            }
        }

        public void AddScope(string globPattern, ScopeAllowActionEnum allowAction)
        {
            Scopes.Add(new Scope(this, globPattern, allowAction));
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
