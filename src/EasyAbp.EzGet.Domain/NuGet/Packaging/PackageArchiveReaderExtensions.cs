using EasyAbp.EzGet.NuGetPackages;
using NuGet.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Guids;

namespace NuGet.Packaging
{
    public static class PackageArchiveReaderExtensions
    {
        public static bool HasReadme(this PackageArchiveReader package)
            => !string.IsNullOrEmpty(package.NuspecReader.GetReadme());

        public static bool HasEmbeddedIcon(this PackageArchiveReader package)
            => !string.IsNullOrEmpty(package.NuspecReader.GetIcon());

        public async static Task<Stream> GetReadmeAsync(
            this PackageArchiveReader package,
            CancellationToken cancellationToken = default)
        {
            var readmePath = package.NuspecReader.GetReadme();
            if (readmePath == null)
            {
                throw new InvalidOperationException("Package does not have a readme!");
            }

            return await package.GetStreamAsync(readmePath, cancellationToken);
        }

        public async static Task<Stream> GetIconAsync(
            this PackageArchiveReader package,
            CancellationToken cancellationToken = default)
        {
            return await package.GetStreamAsync(
                PathUtility.StripLeadingDirectorySeparators(package.NuspecReader.GetIcon()),
                cancellationToken);
        }
    }
}
