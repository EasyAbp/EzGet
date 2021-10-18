using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EzGet.Feeds
{
    public class Feed : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; }
    }
}
