using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EzGet.PackageRegistrations
{
    public class PackageRegistration : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? FeedId { get; }
        public string PackageName { get; }
        public long DownloadCount { get; set; }
        public string PackageType { get; }
        public string LastVersion { get; private set; }
        public long Size { get; set; }
        public string Description { get; set; }
        public Guid? TenantId { get; }

        public PackageRegistration(
            Guid id,
            Guid? feedId,
            [NotNull] string packageName,
            [NotNull] string packageType,
            [NotNull] string lastVersion,
            long size,
            string description,
            Guid? tenantId = null)
        {
            Check.NotNullOrWhiteSpace(packageName, nameof(packageName));
            Check.NotNullOrWhiteSpace(packageType, nameof(packageType));

            Id = id;
            FeedId = feedId;
            PackageName = packageName;
            PackageType = packageType;
            DownloadCount = 0;
            LastVersion = lastVersion;
            Size = size;
            Description = description;
            TenantId = tenantId;
        }

        public void SetLastVersion([NotNull] string version)
        {
            Check.NotNullOrWhiteSpace(version, nameof(version));
            LastVersion = version;
        }
    }
}
