using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.NuGet.Packages
{
    public interface INuGetPackageSearcher
    {
        Task<NuGetPackageSearchPackageListResult> SearchPackageListAsync(
            int skip,
            int take,
            string filter,
            bool includePrerelease,
            bool includeSemVer2,
            string packageType = null,
            string feedName = null,
            CancellationToken cancellationToken = default);

        Task<NuGetPackageSearchNameListResult> SearchNameListAsync(
            int skip,
            int take,
            string filter,
            bool includePrerelease,
            bool includeSemVer2,
            string packageType = null,
            string feedName = null,
            CancellationToken cancellationToken = default);
    }
}
