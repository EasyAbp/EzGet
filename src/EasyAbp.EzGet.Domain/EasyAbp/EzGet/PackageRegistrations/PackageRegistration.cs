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
        public string PackageName { get; }
        public long DownloadCount { get; set; }
        public string PackageType { get; }

        public PackageRegistration(Guid id, [NotNull] string packageName, [NotNull] string packageType)
        {
            Check.NotNullOrWhiteSpace(packageName, nameof(packageName));
            Check.NotNullOrWhiteSpace(packageType, nameof(packageType));

            Id = id;
            PackageName = packageName;
            PackageType = packageType;
            DownloadCount = 0;
        }
    }
}
