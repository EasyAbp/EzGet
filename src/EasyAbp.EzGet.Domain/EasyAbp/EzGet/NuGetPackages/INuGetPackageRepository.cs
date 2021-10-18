using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace EasyAbp.EzGet.NuGetPackages
{
    public interface INuGetPackageRepository : IBasicRepository<NuGetPackage, Guid>
    {
        Task<bool> ExistsAsync(ISpecification<NuGetPackage> specification, CancellationToken cancellationToken = default);
    }
}
