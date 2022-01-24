using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.PackageRegistrations
{
    public interface IPackageRegistrationManager
    {
        Task<PackageRegistration> CreateOrUpdateAsync(
            [NotNull] string packageName,
            [NotNull] string type,
            Guid? feedId,
            [NotNull] string version,
            long size);
    }
}
