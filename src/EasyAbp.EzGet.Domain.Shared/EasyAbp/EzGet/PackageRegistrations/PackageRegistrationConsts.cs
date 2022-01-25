using EasyAbp.EzGet.NuGet.Packages;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.PackageRegistrations
{
    public class PackageRegistrationConsts
    {
        public static int MaxPackageNameLength => NuGetPackageConsts.MaxPackageNameLength;
        public static int MaxPackageTypeLength { get; set; } = 10;
        public static int MaxDescriptionLength => NuGetPackageConsts.MaxDescriptionLength;
        public static int MaxLastVersionLength => NuGetPackageConsts.MaxNormalizedVersionLength;
    }
}
