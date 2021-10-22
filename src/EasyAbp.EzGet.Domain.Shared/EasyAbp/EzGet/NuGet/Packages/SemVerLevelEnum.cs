using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.NuGet.Packages
{
    public enum SemVerLevelEnum
    {
        /// <summary>
        /// Either an invalid semantic version or a semantic version v1.0.0
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// A valid semantic version v2.0.0
        /// </summary>
        SemVer2 = 2
    }
}
