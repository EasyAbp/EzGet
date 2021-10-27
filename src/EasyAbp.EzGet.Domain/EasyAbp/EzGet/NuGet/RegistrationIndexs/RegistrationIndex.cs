using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.NuGet.RegistrationIndexs
{
    public class RegistrationIndex
    {
        public string RegistrationIndexUrl { get; }
        public IReadOnlyList<string> Types { get; }
        public int Count { get; }
        public IReadOnlyList<RegistrationPage> Pages { get; }

        public RegistrationIndex(
            string registrationIndexUrl,
            IReadOnlyList<string> types,
            int count,
            IReadOnlyList<RegistrationPage> pages)
        {
            RegistrationIndexUrl = registrationIndexUrl;
            Types = types;
            Count = count;
            Pages = pages;
        }
    }
}
