using EasyAbp.EzGet.NuGet.Packages;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.NuGet.RegistrationIndexs
{
    public interface IRegistrationIndexBuilder
    {
        Task<RegistrationIndex> BuildIndexAsync([NotNull] IReadOnlyList<NuGetPackage> nuGetPackages, string feedName);
        Task<RegistrationLeaf> BuildLeafAsync([NotNull] NuGetPackage nuGetPackage, string feedName);
    }
}
