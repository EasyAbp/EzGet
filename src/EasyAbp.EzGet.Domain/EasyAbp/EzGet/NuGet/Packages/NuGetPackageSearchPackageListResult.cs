using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.NuGet.Packages
{
    public class NuGetPackageSearchPackageListResult
    {
        public long Count { get; }
        public IReadOnlyList<NuGetPackageSearchResult> Packages { get; }

        public NuGetPackageSearchPackageListResult(long count, IReadOnlyList<NuGetPackageSearchResult> packages)
        {
            Count = count;
            Packages = packages;
        }
    }
}
