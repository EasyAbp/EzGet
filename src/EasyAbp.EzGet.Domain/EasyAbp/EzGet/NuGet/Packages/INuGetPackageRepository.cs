using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace EasyAbp.EzGet.NuGet.Packages
{
    public interface INuGetPackageRepository : IBasicRepository<NuGetPackage, Guid>
    {
        Task<bool> ExistsAsync(
            string packageName,
            string version,
            Guid? feedId,
            bool? listed,
            CancellationToken cancellationToken = default);

        Task<NuGetPackage> GetAsync(
            string packageName,
            string version,
            Guid? feedId,
            bool? listed,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        Task<List<NuGetPackage>> GetListByPackageNameAndFeedIdAsync(
            string packageName,
            Guid? feedId = null,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        Task<List<NuGetPackage>> GetListAsync(
            string filter = null,
            Guid? feedId = null,
            string packageName = null,
            string version = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
            string filter = null,
            Guid? feedId = null,
            string packageName = null,
            string version = null,
            CancellationToken cancellationToken = default);
    }
}
