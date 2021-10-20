using JetBrains.Annotations;
using NuGet.Packaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EzGet.NuGetPackages
{
    public interface INuGetPackageManager : IDomainService
    {
        Task<NuGetPackage> CreateAsync([NotNull] PackageArchiveReader packageReader);
    }
}
