using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Admin.NuGet.Packages
{
    public class GetNuGetPackagesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public string PackageName { get; set; }
        public string Version { get; set; }
        public Guid? FeedId { get; set; }
    }
}
