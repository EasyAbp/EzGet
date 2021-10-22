using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.NuGet.Packages
{
    public class PackageTypeDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }
}
