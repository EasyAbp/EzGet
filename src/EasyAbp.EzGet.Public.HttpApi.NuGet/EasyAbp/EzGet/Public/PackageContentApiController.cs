using EasyAbp.EzGet.NuGet.ServiceIndexs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EasyAbp.EzGet.Public.NuGet.Models;
using EasyAbp.EzGet.Public.NuGet.Packages;
using Microsoft.AspNetCore.Authorization;
using EasyAbp.EzGet.AspNetCore.Authentication;

namespace EasyAbp.EzGet.Public.NuGet
{
    //See: https://docs.microsoft.com/en-us/nuget/api/package-base-address-resource
    [AllowAnonymous]
    [Authorize(AuthenticationSchemes = EzGetAspNetCoreAuthenticationConsts.EzGetBasicAuthenticationScheme)]
    public class PackageContentApiController : EzGetHttpApiNuGetControllerBase
    {
        protected INuGetPackagePublicAppService NuGetPackagePublicAppService { get; }

        public PackageContentApiController(INuGetPackagePublicAppService nuGetPackagePublicAppService)
        {
            NuGetPackagePublicAppService = nuGetPackagePublicAppService;
        }

        public virtual async Task<IActionResult> GetVersionsAsync(string id, string feedName = null)
        {
            var versionList = await NuGetPackagePublicAppService.GetVersionListByPackageName(id, feedName);
            return new JsonResult(new VersionsModel { Versions = versionList });
        }

        public virtual async Task<IActionResult> GetPackageContentAsync(string id, string version, string feedName = null)
        {
            var content = await NuGetPackagePublicAppService.GetPackageContentAsync(id, version, feedName);

            if(content == null)
                return NotFound();

            return File(content, "application/octet-stream");
        }

        public virtual async Task<IActionResult> GetPackageManifestAsync(string id, string version, string feedName = null)
        {
            var manifest = await NuGetPackagePublicAppService.GetPackageManifestAsync(id, version, feedName);

            if (manifest == null)
                return NotFound();

            return File(manifest, "text/xml");
        }
    }
}
