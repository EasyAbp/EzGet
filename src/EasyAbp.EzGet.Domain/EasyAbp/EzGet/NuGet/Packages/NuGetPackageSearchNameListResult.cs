using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.NuGet.Packages
{
    public class NuGetPackageSearchNameListResult
    {
        public long Count { get; }
        public IReadOnlyList<string> Names { get; }

        public NuGetPackageSearchNameListResult(long count, IReadOnlyList<string> names)
        {
            Count = count;
            Names = names;
        }
    }
}
