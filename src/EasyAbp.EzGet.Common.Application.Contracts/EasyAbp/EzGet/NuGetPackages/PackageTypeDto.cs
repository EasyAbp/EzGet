using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.NuGetPackages
{
    public class PackageTypeDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }
}
