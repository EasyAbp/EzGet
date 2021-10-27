using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.NuGet.RegistrationIndexs
{
    public class RegistrationPageItem
    {
        public string RegistrationLeafUrl { get; }
        public string PackageContentUrl { get; }
        public NuGetPackageMetadata PackageMetadata { get; }

        public RegistrationPageItem(
            string registrationLeafUrl,
            string packageContentUrl,
            NuGetPackageMetadata packageMetadata)
        {
            RegistrationLeafUrl = registrationLeafUrl;
            PackageContentUrl = packageContentUrl;
            PackageMetadata = packageMetadata;
        }
    }
}
