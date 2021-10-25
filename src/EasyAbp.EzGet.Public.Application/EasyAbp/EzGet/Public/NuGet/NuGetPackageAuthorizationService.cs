using EasyAbp.EzGet.Public.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EzGet.Public.NuGet
{
    [ExposeServices(typeof(INuGetPackageAuthorizationService))]
    public class NuGetPackageAuthorizationService : INuGetPackageAuthorizationService, ITransientDependency
    {
        protected IAuthorizationService AuthorizationService { get; }

        public NuGetPackageAuthorizationService(IAuthorizationService authorizationService)
        {
            AuthorizationService = authorizationService;
        }

        public virtual async Task CheckDefaultAsync()
        {
            await AuthorizationService.CheckAsync(EzGetPublicPermissions.NuGetPackages.Default);
        }

        public virtual async Task CheckCreationAsync()
        {
            await AuthorizationService.CheckAsync(EzGetPublicPermissions.NuGetPackages.Create);
        }

        public virtual async Task CheckUnlistAsync()
        {
            await AuthorizationService.CheckAsync(EzGetPublicPermissions.NuGetPackages.Unlist);
        }

        public virtual async Task CheckRelistAsync()
        {
            await AuthorizationService.CheckAsync(EzGetPublicPermissions.NuGetPackages.Relist);
        }
    }
}
