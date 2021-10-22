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

        internal TargetFramework([NotNull] NuGetPackage package, string moniker)
        {
            Check.NotNull(package, nameof(package));

            PackageId = package.Id;
            Moniker = moniker;
        }
    }
}
