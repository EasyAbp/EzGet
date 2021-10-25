using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.NuGet
{
    public interface INuGetPackageAuthorizationService 
    {
        Task CheckDefaultAsync();
        Task CheckCreationAsync();
        Task CheckUnlistAsync();
        Task CheckRelistAsync();
    }
}
