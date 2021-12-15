using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EzGet.NuGet.Packages
{
    public class TargetFramework : Entity<Guid>
    {
        public Guid PackageId { get; }
        public string Moniker { get; }

        private TargetFramework()
        {
        }

        public TargetFramework([NotNull] NuGetPackage package, Guid id, string moniker)
        {
            Check.NotNull(package, nameof(package));

            Id = id;
            PackageId = package.Id;
            Moniker = moniker;
        }
    }
}
