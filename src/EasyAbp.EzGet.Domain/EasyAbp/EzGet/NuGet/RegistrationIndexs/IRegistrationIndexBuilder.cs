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
        Task<RegistrationIndex> BuildAsync([NotNull] IReadOnlyList<NuGetPackage> nuGetPackages);
    }
}
