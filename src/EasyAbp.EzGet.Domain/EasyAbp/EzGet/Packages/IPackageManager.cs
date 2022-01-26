using EasyAbp.EzGet.PackageRegistrations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EzGet.Packages
{
    public interface IPackageManager : IDomainService
    {
        Task DeleteLatestAsync(PackageRegistration packageRegistration);
        Task DeleteAllButLatestAsync(PackageRegistration packageRegistration);
        Task DeleteAllAsync(PackageRegistration packageRegistration);
    }
}
