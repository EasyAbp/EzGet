using EasyAbp.EzGet.Permissions;
using EasyAbp.EzGet.Public.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.NuGetPackages
{
    public class NuGetPackageAuthorizationService : INuGetPackageAuthorizationService
    {
        protected IAuthorizationService AuthorizationService { get; }

        public NuGetPackageAuthorizationService(IAuthorizationService authorizationService)
        {
            AuthorizationService = authorizationService;
        }

        public virtual async Task<bool> IsGrantedCreationAsync()
        {
            return await AuthorizationService.IsGrantedAsync(EzGetPublicPermissions.NuGetPackages.Create);
        }

        public virtual async Task CheckCreationAsync()
        {
            await AuthorizationService.CheckAsync(EzGetPublicPermissions.NuGetPackages.Create);
        }
    }
}
