using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.NuGet.Packages
{
    public class PackageDependencyDto : EntityDto<Guid>
    {
        public string DependencyPackageName { get; set; }
        public string VersionRange { get; set; }
        public string TargetFramework { get; set; }
    }
}
