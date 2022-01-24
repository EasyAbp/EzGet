using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Public.PackageRegistrations
{
    public class PackageRegistrationDto : FullAuditedEntityDto<Guid>
    {
        public Guid? FeedId { get; set; }
        public string PackageName { get; set; }
        public long DownloadCount { get; set; }
        public string PackageType { get; set; }
        public string LastVersion { get; set; }
        public long Size { get; set; }
    }
}
