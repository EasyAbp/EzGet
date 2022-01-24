using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EzGet.PackageRegistrations
{
    public class PackageRegistration : FullAuditedAggregateRoot<Guid>
    {
        public Guid? FeedId { get; }
        public string PackageName { get; }
        public long DownloadCount { get; set; }
        public string PackageType { get; }
        public string LastVersion { get; private set; }
        public long Size { get; set; }

        public PackageRegistration(
            Guid id,
            Guid? feedId,
            [NotNull] string packageName,
            [NotNull] string packageType,
            [NotNull] string lastVersion,
            long size)
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
        }

        public void SetLastVersion([NotNull] string version)
        {
            Check.NotNullOrWhiteSpace(version, nameof(version));
            LastVersion = version;
        }
    }
}
