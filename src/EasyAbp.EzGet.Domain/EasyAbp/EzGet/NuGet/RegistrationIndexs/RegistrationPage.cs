using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.NuGet.RegistrationIndexs
{
    public class RegistrationPage
    {
        public string RegistrationPageUrl { get; }
        public int Count { get; }
        public string Lower { get; }
        public string Upper { get; }
        public IReadOnlyList<RegistrationPageItem> PageItems { get; }
    }
}
