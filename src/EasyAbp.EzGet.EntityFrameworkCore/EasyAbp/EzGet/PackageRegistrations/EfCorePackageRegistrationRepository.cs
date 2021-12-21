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

        public async Task<PackageRegistration> FindByNameAndTypeAsync(
            string name,
            string type,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(p => p.PackageName == name && p.PackageType == type)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }
    }
}
