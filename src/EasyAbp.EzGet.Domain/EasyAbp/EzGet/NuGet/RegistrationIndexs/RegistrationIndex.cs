using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.NuGet.RegistrationIndexs
{
    public class RegistrationIndex
    {
        public string RegistrationIndexUrl { get; }
        public IReadOnlyList<string> Type { get; }
        public int Count { get; }
        public IReadOnlyList<RegistrationPage> Pages { get; }
    }
}
