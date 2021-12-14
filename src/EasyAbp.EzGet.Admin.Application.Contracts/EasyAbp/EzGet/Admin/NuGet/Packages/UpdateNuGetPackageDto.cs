using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EzGet.Admin.NuGet.Packages
{
    public class UpdateNuGetPackageDto : IHasConcurrencyStamp
    {
        public bool Listed { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
