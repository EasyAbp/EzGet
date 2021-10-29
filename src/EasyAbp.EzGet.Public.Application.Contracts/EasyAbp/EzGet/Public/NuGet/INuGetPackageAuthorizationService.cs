using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.NuGet
{
    //TODO: Plan to remove this service, use AuthenticationHandler instead
    public interface INuGetPackageAuthorizationService 
    {
        Task CheckDefaultAsync();
        Task CheckCreationAsync();
        Task CheckUnlistAsync();
        Task CheckRelistAsync();
    }
}
