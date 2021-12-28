using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.Public.NuGet.Packages
{
    public class GetVersionListInput
    {
        public bool? IncludePrerelease { get; set; }
        public bool? IncludeSemVer2 { get; set; }
        public string PackageName { get; set; }
        public string FeedName { get; set; }
    }
}
