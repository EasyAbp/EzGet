using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.NuGet.RegistrationIndexs
{
    public class RegistrationLeaf
    {
        public string RegistrationLeafUrl { get; }
        public bool Listed { get; }
        public string PackageContentUrl { get; }
        public DateTimeOffset Published { get; }
        public string RegistrationIndexUrl { get; }

        public RegistrationLeaf(
            string registrationLeafUrl,
            bool listed,
            string packageContentUrl,
            DateTimeOffset published,
            string registrationIndexUrl)
        {
            RegistrationLeafUrl = registrationLeafUrl;
            Listed = listed;
            PackageContentUrl = packageContentUrl;
            Published = Listed ? published : default(DateTimeOffset);
            RegistrationIndexUrl = registrationIndexUrl;
        }
    }
}
