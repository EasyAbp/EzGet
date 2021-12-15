using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EzGet.NuGet.Packages
{
    public class PackageType : Entity<Guid>
    {
        public Guid PackageId { get; }
        public string Name { get; }
        public string Version { get; }

        private PackageType()
        {
        }

        public PackageType([NotNull] NuGetPackage package, Guid id, string name, string version)
        {
            Check.NotNull(package, nameof(package));

            Id = id;
            PackageId = package.Id;
            Name = name;
            Version = version;
        }
    }
}
