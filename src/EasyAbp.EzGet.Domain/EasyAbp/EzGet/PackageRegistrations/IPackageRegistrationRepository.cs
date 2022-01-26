using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EzGet.PackageRegistrations
{
    public interface IPackageRegistrationRepository : IBasicRepository<PackageRegistration, Guid>
    {
        Task<PackageRegistration> FindByNameAndTypeAsync(
            string name,
            string type,
            Guid? feedId = null,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        Task<List<PackageRegistration>> GetListAsync(
            string filter = null,
            Guid? feedId = null,
            Guid? userId = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string sorting = null,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
            string filter = null,
            Guid? feedId = null,
            Guid? userId = null,
            CancellationToken cancellationToken = default);
    }
}
