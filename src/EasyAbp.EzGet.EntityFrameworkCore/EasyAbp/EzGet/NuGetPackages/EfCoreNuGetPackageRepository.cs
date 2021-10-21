using EasyAbp.EzGet.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Specifications;

namespace EasyAbp.EzGet.NuGetPackages
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
            return await (await GetDbSetAsync())
                .Where(specification.ToExpression())
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
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
