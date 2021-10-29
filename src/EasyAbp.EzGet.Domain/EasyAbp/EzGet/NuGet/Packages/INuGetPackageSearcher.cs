using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.NuGet.Packages
{
    public interface INuGetPackageSearcher
    {
        Task<NuGetPackageSearchListResult> SearchListAsync(
            int skip,
            int take,
            string filter,
            bool includePrerelease,
            bool includeSemVer2,
            string packageType = null,
            CancellationToken cancellationToken = default);
    }
}
