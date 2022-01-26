using EasyAbp.EzGet.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace EasyAbp.EzGet.PackageRegistrations
{
    public class EfCorePackageRegistrationRepository :
        EfCoreRepository<IEzGetDbContext, PackageRegistration, Guid>,
        IPackageRegistrationRepository
    {
        public EfCorePackageRegistrationRepository(IDbContextProvider<IEzGetDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<PackageRegistration> FindByNameAndTypeAsync(
            string name,
            string type,
            Guid? feedId = null,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await (includeDetails ? (await GetDbSetAsync()) : (await WithDetailsAsync()))
                .Where(p => p.PackageName == name && p.PackageType == type && p.FeedId == feedId)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<PackageRegistration>> GetListAsync(
            string filter = null,
            Guid? feedId = null,
            Guid? userId = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string sorting = null,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            return await (await WithUserIdAsync(userId, false))
                .IncludeDetails(includeDetails)
                .WhereIf(!string.IsNullOrWhiteSpace(filter), p => p.PackageName.Contains(filter))
                .Where(p => p.FeedId == feedId)
                .OrderBy(string.IsNullOrWhiteSpace(sorting) ? nameof(PackageRegistration.CreationTime) : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string filter = null,
            Guid? feedId = null,
            Guid? userId = null,
            CancellationToken cancellationToken = default)
        {
            return await (await WithUserIdAsync(userId, false))
                .WhereIf(!string.IsNullOrWhiteSpace(filter), p => p.PackageName.Contains(filter))
                .Where(p => p.FeedId == feedId)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public override async Task<IQueryable<PackageRegistration>> WithDetailsAsync()
        {
            return (await GetDbSetAsync()).IncludeDetails();
        }

        protected virtual async Task<IQueryable<PackageRegistration>> WithUserIdAsync(Guid? userId, bool includeDetails)
        {
            var dbContext = await GetDbContextAsync();

            if (!userId.HasValue)
            {
                return dbContext.PackageRegistrations.IncludeDetails(includeDetails);
            }

            return from packageRegistration in dbContext.PackageRegistrations.IncludeDetails(includeDetails)
                   join packageRegistrationUser in dbContext.Set<PackageRegistrationUser>() on packageRegistration.Id equals packageRegistrationUser.PackageRegistrationId
                   where packageRegistrationUser.UserId == userId
                   select packageRegistration;
        }
    }
}
