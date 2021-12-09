using EasyAbp.EzGet.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
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

        public virtual async Task<bool> ExistsAsync(ISpecification<NuGetPackage> specification, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(specification.ToExpression())
                .AnyAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<NuGetPackage> GetAsync(
            ISpecification<NuGetPackage> specification,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await (includeDetails ? (await WithDetailsAsync()) : (await GetQueryableAsync()))
                .Where(specification.ToExpression())
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<NuGetPackage>> GetListByPackageNameAndFeedIdAsync(
            string packageName,
            Guid? feedId = null,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await (includeDetails ? (await WithDetailsAsync()) : (await GetQueryableAsync()))
                .Where(p => p.PackageName == packageName)
                .Where(p => p.FeedId == feedId)
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
            return await (includeDetails ? (await GetDbSetAsync()) : (await WithDetailsAsync()))
                .Where(p => p.FeedId == feedId)
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
            return await (await GetQueryableAsync())
                .Where(p => p.FeedId == feedId)
                .WhereIf(!string.IsNullOrWhiteSpace(filter), p => p.PackageName.Contains(filter))
                .WhereIf(!string.IsNullOrWhiteSpace(packageName), p => p.PackageName == packageName)
                .WhereIf(!string.IsNullOrWhiteSpace(version), p => p.NormalizedVersion == version)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public override async Task<IQueryable<NuGetPackage>> WithDetailsAsync()
        {
            return (await GetQueryableAsync())
                .Include(p => p.PackageTypes)
                .Include(p => p.Dependencies)
                .Include(p => p.TargetFrameworks);
        }
    }
}
