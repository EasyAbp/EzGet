using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EzGet.NuGetPackages
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

        internal PackageDependency([NotNull] NuGetPackage package, string dependencyPackageName, string versionRange, string targetFramework)
        {
            Check.NotNull(package, nameof(package));

            PackageId = package.Id;
            DependencyPackageName = dependencyPackageName;
            VersionRange = versionRange;
            TargetFramework = targetFramework;
        }
    }
}
