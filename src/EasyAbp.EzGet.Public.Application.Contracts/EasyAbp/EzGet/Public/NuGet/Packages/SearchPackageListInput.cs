using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Public.NuGet.Packages
{
    public class SearchPackageListInput : PagedResultRequestDto
    {
        public string Filter { get; set; }
        public bool IncludePrerelease { get; set; }
        public bool IncludeSemVer2 { get; set; }
        public string PackageType { get; set; }
        public string FeedName { get; set; }
    }
}
