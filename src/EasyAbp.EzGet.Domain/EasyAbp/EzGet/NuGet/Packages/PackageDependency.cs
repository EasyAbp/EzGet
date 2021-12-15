using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EzGet.NuGet.Packages
{
    public class PackageDependency : Entity<Guid>
    {
        public Guid PackageId { get; }
        public string DependencyPackageName { get; }
        public string VersionRange { get; }
        public string TargetFramework { get; }

        private PackageDependency()
        {
        }

        public PackageDependency([NotNull] NuGetPackage package, Guid id, string dependencyPackageName, string versionRange, string targetFramework)
        {
            Check.NotNull(package, nameof(package));

            Id = id;
            PackageId = package.Id;
            DependencyPackageName = dependencyPackageName;
            VersionRange = versionRange;
            TargetFramework = targetFramework;
        }
    }
}
