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
        Task<PackageRegistration> FindByNameAndTypeAsync(string name, string type, CancellationToken cancellationToken = default);
    }
}
