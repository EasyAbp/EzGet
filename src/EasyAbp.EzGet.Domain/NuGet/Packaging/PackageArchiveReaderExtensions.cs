using EasyAbp.EzGet.NuGetPackages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volo.Abp.Guids;

namespace NuGet.Packaging
{
    public static class PackageArchiveReaderExtensions
    {
        public static bool HasReadme(this PackageArchiveReader package)
            => !string.IsNullOrEmpty(package.NuspecReader.GetReadme());

        public static bool HasEmbeddedIcon(this PackageArchiveReader package)
            => !string.IsNullOrEmpty(package.NuspecReader.GetIcon());
    }
}
