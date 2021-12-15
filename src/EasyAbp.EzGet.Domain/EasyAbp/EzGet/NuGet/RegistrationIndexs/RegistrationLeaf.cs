using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.NuGet.RegistrationIndexs
{
    public class RegistrationLeaf
    {
        public string RegistrationLeafUrl { get; }
        public IReadOnlyList<string> Types { get; }
        public bool Listed { get; }
        public string PackageContentUrl { get; }
        public DateTimeOffset Published { get; }
        public string RegistrationIndexUrl { get; }

        public RegistrationLeaf(
            string registrationLeafUrl,
            bool listed,
            string packageContentUrl,
            DateTimeOffset published,
            string registrationIndexUrl,
            IReadOnlyList<string> types = null)
        {
            RegistrationLeafUrl = registrationLeafUrl;
            Listed = listed;
            PackageContentUrl = packageContentUrl;
            Published = Listed ? published : default(DateTimeOffset);
            RegistrationIndexUrl = registrationIndexUrl;
            Types = types ?? DefaultTypes;
        }

        private static readonly IReadOnlyList<string> DefaultTypes = new List<string>
        {
            "Package",
            "http://schema.nuget.org/catalog#Permalink"
        };
    }
}
