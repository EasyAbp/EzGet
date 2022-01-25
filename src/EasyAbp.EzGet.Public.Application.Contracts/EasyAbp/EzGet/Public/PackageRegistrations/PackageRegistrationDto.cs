using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EzGet.Public.PackageRegistrations
{
    public class PackageRegistrationDto : FullAuditedEntityDto<Guid>, IMultiTenant
    {
        public Guid? FeedId { get; set; }
        public string PackageName { get; set; }
        public long DownloadCount { get; set; }
        public string PackageType { get; set; }
        public string LastVersion { get; set; }
        public long Size { get; set; }
        public string Description { get; set; }
        public Guid? TenantId { get; set; }
    }
}
