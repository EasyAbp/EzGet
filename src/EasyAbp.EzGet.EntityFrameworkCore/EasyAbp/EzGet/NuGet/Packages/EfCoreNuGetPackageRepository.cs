using EasyAbp.EzGet.EntityFrameworkCore;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Specifications;

namespace EasyAbp.EzGet.NuGet.Packages
{
    public class EfCoreNuGetPackageRepository : EfCoreRepository<IEzGetDbContext, NuGetPackage, Guid>, INuGetPackageRepository
    {
        public EfCoreNuGetPackageRepository(IDbContextProvider<IEzGetDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<bool> ExistsAsync(
            string packageName,
            string version,
            Guid? feedId,
            bool? listed,
            CancellationToken cancellationToken = default)
        {
            return await (await GetFeedQueryableAsync(feedId, false))
                .Where(p => p.PackageName == packageName && p.NormalizedVersion == version)
                .WhereIf(listed.HasValue, p => p.Listed == listed)
                .AnyAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<NuGetPackage> FindAsync(
            [NotNull] string packageName,
            [NotNull] string version,
            Guid? feedId,
            bool? listed,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            Check.NotNullOrWhiteSpace(packageName, nameof(packageName));
            Check.NotNullOrWhiteSpace(version, nameof(version));

            return await (await GetFeedQueryableAsync(feedId, includeDetails))
                .Where(p => p.PackageName == packageName && p.NormalizedVersion == version)
                .WhereIf(listed.HasValue, p => p.Listed == listed)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<NuGetPackage>> GetListByPackageNameAndFeedIdAsync(
            [NotNull] string packageName,
            bool? includePrerelease = null,
            bool? includeSemVer2 = null,
            Guid? feedId = null,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            Check.NotNullOrWhiteSpace(packageName, nameof(packageName));

            return await (await GetFeedQueryableAsync(feedId, includeDetails))
                .Where(p => p.PackageName == packageName)
                .WhereIf(null != includePrerelease, p => p.IsPrerelease == includePrerelease)
                .WhereIf(null != includeSemVer2 && !includeSemVer2.Value, p => p.SemVerLevel != SemVerLevelEnum.SemVer2)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<NuGetPackage>> GetListAsync(
            string filter = null,
            Guid? feedId = null,
            string packageName = null,
            string version = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await (await GetFeedQueryableAsync(feedId, includeDetails))
                .WhereIf(!string.IsNullOrWhiteSpace(filter), p => p.PackageName.Contains(filter))
                .WhereIf(!string.IsNullOrWhiteSpace(packageName), p => p.PackageName == packageName)
                .WhereIf(!string.IsNullOrWhiteSpace(version), p => p.NormalizedVersion == version)
                .OrderBy(string.IsNullOrWhiteSpace(sorting) ? nameof(NuGetPackage.CreationTime) : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string filter = null,
            Guid? feedId = null,
            string packageName = null,
            string version = null,
            CancellationToken cancellationToken = default)
        {
            return await (await GetFeedQueryableAsync(feedId, false))
                .WhereIf(!string.IsNullOrWhiteSpace(filter), p => p.PackageName.Contains(filter))
                .WhereIf(!string.IsNullOrWhiteSpace(packageName), p => p.PackageName == packageName)
                .WhereIf(!string.IsNullOrWhiteSpace(version), p => p.NormalizedVersion == version)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public override async Task<IQueryable<NuGetPackage>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }


        private async Task<IQueryable<NuGetPackage>> GetFeedQueryableAsync(Guid? feedId, bool includeDetails)
        {
            var dbContext = await GetDbContextAsync();
            return from package in dbContext.NuGetPackages.IncludeDetails(includeDetails)
                   join registration in dbContext.PackageRegistrations on package.PackageRegistrationId equals registration.Id
                   where registration.FeedId == feedId
                   select package;
        }
    }
}
