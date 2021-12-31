using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EasyAbp.EzGet.Public.NuGet.Packages
{
    public class NuGetPackageSearchNameListResultDto
    {
        public long Count { get; set; }

        public IEnumerable<string> Names { get; set; }
    }
}
