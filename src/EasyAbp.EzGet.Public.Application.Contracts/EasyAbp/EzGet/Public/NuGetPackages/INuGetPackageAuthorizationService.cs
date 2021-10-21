using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.NuGetPackages
{
    public interface INuGetPackageAuthorizationService 
    {
        Task<bool> IsGrantedCreationAsync();
        Task CheckCreationAsync();
    }
}
