using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.NuGet.Packages
{
    public class NuGetPackageSearchListResult
    {
        public long Count { get; }
        public IReadOnlyList<NuGetPackageSearchResult> Packages { get; set; }

        public NuGetPackageSearchListResult(long count, IReadOnlyList<NuGetPackageSearchResult> packages)
        {
            Count = count;
            Packages = packages;
        }
    }
}
