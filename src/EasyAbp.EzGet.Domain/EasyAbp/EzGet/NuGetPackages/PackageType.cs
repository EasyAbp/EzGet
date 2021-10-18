using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EzGet.NuGetPackages
{
    public class PackageType : Entity<Guid>
    {
        public Guid PackageId { get; }
        public string Name { get; }
        public string Version { get; }

        private PackageType()
        {
        }

        internal PackageType([NotNull] NuGetPackage package, string name, string version)
        {
            Check.NotNull(package, nameof(package));

            PackageId = package.Id;
            Name = name;
            Version = version;
        }
    }
}
