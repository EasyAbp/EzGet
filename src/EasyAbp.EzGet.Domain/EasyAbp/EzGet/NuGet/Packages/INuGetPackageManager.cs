using JetBrains.Annotations;
using NuGet.Packaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Specifications;

namespace EasyAbp.EzGet.NuGet.Packages
{
    public interface INuGetPackageManager : IDomainService
    {
        Task<NuGetPackage> CreateAsync(
            [NotNull] PackageArchiveReader packageReader,
            long size,
            string feedName = null);

        Task<string> GetNupkgBlobNameAsync([NotNull] NuGetPackage package);

        Task<string> GetNuspecBlobNameAsync([NotNull] NuGetPackage package);

        Task<string> GetReadmeBlobNameAsync([NotNull] NuGetPackage package);

        Task<string> GetIconBlobNameAsync([NotNull] NuGetPackage package);
    }
}
